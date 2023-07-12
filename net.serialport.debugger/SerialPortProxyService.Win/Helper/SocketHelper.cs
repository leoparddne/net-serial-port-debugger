using System.Net.Sockets;

namespace SerialPortProxyService.Win.Helper
{
    public class SocketHelper
    {
        private Socket Socket { get; set; }

        public SocketHelper()
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
    }
}
