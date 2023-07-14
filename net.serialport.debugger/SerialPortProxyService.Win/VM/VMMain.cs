using PropertyChanged;
using SerialPortProxyService.Win.Helper;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Text;

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

        public VMMain()
        {
            var sysSerialPortList = SerialPort.GetPortNames();
            if (sysSerialPortList != null)
            {
                SerialPortList = new(sysSerialPortList);
            }

            SerialPortHelper = new SerialPortHelper("COM2", 9600, Parity.None, 8, StopBits.One);
            SerialPortHelper.DataReceivedEvent += SerialPortHelper_DataReceivedEvent;
            SerialPortHelper.Open();
        }

        private void SerialPortHelper_DataReceivedEvent(byte[] byteData)
        {
            var str = Encoding.Default.GetString(byteData, 0, byteData.Length);
            //File.WriteAllText("./log.txt", str.Trim());
        }
    }
}
