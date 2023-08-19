using System.IO.Ports;
using System.Text;

namespace SerialPortProxyService.Common.Model
{
    public class SerialPortProxyConfig : IProxyConfig
    {
        public Encoding Encode { get; set; }
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }
    }
}
