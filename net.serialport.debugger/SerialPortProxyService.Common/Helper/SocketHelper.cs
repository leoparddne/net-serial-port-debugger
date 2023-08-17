using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SerialPortProxyService.Common.Helper
{
    public class SocketHelper : IDisposable
    {
        private Encoding Encode { get; set; }
        private Socket SocketInstance { get; set; }

        public SocketHelper(Encoding encode)
        {
            Encode = encode;
            SocketInstance = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public SocketHelper(Encoding encode, Socket socket)
        {
            Encode = encode;
            SocketInstance = socket;
        }


        public int Send(string msg)
        {
            var byteStr = Encode.GetBytes(msg);

            return SocketInstance.Send(byteStr);
        }

        public void Bind(string ip, int port)
        {
            IPAddress iPAddress = IPAddress.Parse(ip);
            var ipEndPoint = new IPEndPoint(iPAddress, port);
            SocketInstance.Bind(ipEndPoint);

        }

        public void Connect(string ip, int port)
        {
            IPAddress iPAddress = IPAddress.Parse(ip);
            var ipEndPoint = new IPEndPoint(iPAddress, port);
            SocketInstance.Connect(ipEndPoint);
        }

        public void Listen()
        {
            SocketInstance.Listen();
        }

        public Socket Accept()
        {
            return SocketInstance.Accept();
        }

        public int Receive(byte[] buffer)
        {
            return SocketInstance.Receive(buffer);
        }

        public void Dispose()
        {
            if (SocketInstance.Connected)
            {
                SocketInstance.Close();
            }
            SocketInstance.Dispose();
        }
    }
}
