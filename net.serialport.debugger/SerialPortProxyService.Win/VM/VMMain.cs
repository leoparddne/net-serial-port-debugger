using PropertyChanged;
using SerialPortProxyService.Common.Helper;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Input;

namespace SerialPortProxyService.Win.VM
{
    [AddINotifyPropertyChangedInterface]
    public class VMMain
    {
        private SerialPortHelper SerialPortHelper;

        public ObservableCollection<string> SerialPortList { get; set; }

        /// <summary>
        /// 选中的串口
        /// </summary>
        public string SelectSerialPort { get; set; }

        public ICommand ListenSerialCommand { get; set; }
        public ICommand ListenNetCommand { get; set; }


        public VMMain()
        {
            var sysSerialPortList = SerialPort.GetPortNames();
            if (sysSerialPortList != null)
            {
                SerialPortList = new(sysSerialPortList);
            }
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var encode = System.Text.Encoding.GetEncoding("GB2312");
            SerialPortHelper = new SerialPortHelper(encode, "COM2", 9600, Parity.None, 8, StopBits.One);
            SerialPortHelper.DataReceivedEvent += SerialPortHelper_DataReceivedEvent;
            SerialPortHelper.Open();

            var byteStr = encode.GetBytes("test123!@#中文.");

            SerialPortHelper.Send(byteStr, 0, byteStr.Length);
        }

        private void SerialPortHelper_DataReceivedEvent(byte[] byteData)
        {
            var str = System.Text.Encoding.GetEncoding("GB2312").GetString(byteData, 0, byteData.Length);
            //var str = Encoding.UTF8.GetString(byteData, 0, byteData.Length);
            File.WriteAllText("./log.txt", str.Trim());
        }
    }
}
