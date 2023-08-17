using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SerialPortProxyService.Common.Helper
{
    public class SocketHelper : IDisposable
    {
        private Encoding Encode { get; set; }
        private Socket SocketInstance { get; set; }

        IPEndPoint ipEndPoint;

        public SocketHelper(Encoding encode, string ip, int port)
        {
            IPAddress iPAddress = IPAddress.Parse(ip);
            ipEndPoint = new IPEndPoint(iPAddress, port);

            Encode = encode;
            SocketInstance = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }


        public void Send(string msg)
        {
            var byteStr = Encode.GetBytes(msg);

            SocketInstance.Send(byteStr);
        }

        public void Bind()
        {
            SocketInstance.Bind(ipEndPoint);

        }

        public void Connect(string ip, int port)
        {
            SocketInstance.Connect(ipEndPoint);
        }

        public void Listen()
        {
            SocketInstance.Listen();
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
