using SerialPortProxyService.Common.Constant;
using System.IO.Ports;

namespace SerialPortProxyService
{
    public class ServiceConfig
    {
        public RunningModeEnum Mode { get; set; }
        public Net Net { get; set; }
        public Serial Serial { get; set; }
    }

    public class Net
    {
        public string IP { get; set; }
        public int Port { get; set; }
    }

    public class Serial
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }
    }

}
