using PropertyChanged;
using SerialPortProxyService.Common.Helper;
using SerialPortProxyService.Win.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SerialPortProxyService.Win.VM
{
    [AddINotifyPropertyChangedInterface]
    public class VMMain
    {
        private SerialPortHelper SerialPortHelper;

        public ObservableCollection<string> SerialPortList { get; set; }

        public Dictionary<string, int> ParityList { get; set; }

        public KeyValuePair<string, int> SelectParity { get; set; }

        public Dictionary<string, int> StopBitsList { get; set; }

        public KeyValuePair<string, int> SelectStopBits { get; set; }


        /// <summary>
        /// 选中的串口
        /// </summary>
        public string SelectSerialPort { get; set; }


        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }

        public ICommand OpenServiceCommand { get; set; }


        public VMMain()
        {
            var sysSerialPortList = SerialPort.GetPortNames();
            if (sysSerialPortList != null)
            {
                SerialPortList = new(sysSerialPortList);
            }

            ParityList = EnumHelper.GetEnumInfo(typeof(Parity));
            StopBitsList = EnumHelper.GetEnumInfo(typeof(StopBits));


            OpenServiceCommand = new BaseCommand((para) =>
            {
                Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                var encode = System.Text.Encoding.GetEncoding("GB2312");
                SerialPortHelper = new SerialPortHelper(encode, "COM2", 9600, Parity.None, 8, StopBits.One);
                //SerialPortHelper.DataReceivedEvent += SerialPortHelper_DataReceivedEvent;
                SerialPortHelper.ReceiveCallback = SerialPortHelper_DataReceivedEvent;
                SerialPortHelper.Open();

                var byteStr = encode.GetBytes("test123!@#中文.");

                SerialPortHelper.Send(byteStr, 0, byteStr.Length);
            });
        }

        private void SerialPortHelper_DataReceivedEvent(byte[] byteData)
        {
            var str = System.Text.Encoding.GetEncoding("GB2312").GetString(byteData, 0, byteData.Length);
            //var str = Encoding.UTF8.GetString(byteData, 0, byteData.Length);
            File.WriteAllText("./log.txt", str.Trim());
        }
    }
}
