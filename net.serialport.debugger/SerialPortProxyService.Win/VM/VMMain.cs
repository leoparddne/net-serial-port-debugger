using PropertyChanged;
using System.Collections.ObjectModel;
using System.IO.Ports;

namespace SerialPortProxyService.Win.VM
{
    [AddINotifyPropertyChangedInterface]
    public class VMMain
    {
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
        }
    }
}
