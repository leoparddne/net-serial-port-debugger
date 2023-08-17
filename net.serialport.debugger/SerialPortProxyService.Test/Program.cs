using SerialPortProxyService.Common.Helper;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SerialPortProxyService.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() =>
            {
                StartServer("127.0.0.1", 5000);
            });

            Task.Run(() =>
            {
                StartClient("127.0.0.1", 5000);
            });

            Console.ReadLine();
        }


        static void StartServer(string ip, int port)
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var encode = System.Text.Encoding.GetEncoding("GB2312");

            SocketHelper socketHelper = new SocketHelper(encode);
            socketHelper.Bind(ip, port);

            socketHelper.Listen();


            var acceptSocket = socketHelper.Accept();

            SocketHelper acceptSocketHelper = new SocketHelper(encode, acceptSocket);


            while (true)
            {
                var buffer = new byte[4096];
                var size = acceptSocketHelper.Receive(buffer);
                if (size <= 0)
                {
                    continue;
                }

                var str = encode.GetString(buffer);
                Console.WriteLine($"receive:{str}");
            }
        }

        static void StartClient(string ip, int port)
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var encode = System.Text.Encoding.GetEncoding("GB2312");

            SocketHelper socketHelper = new SocketHelper(encode);
            socketHelper.Connect(ip, port);
            while (true)
            {
                var sendStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var size = socketHelper.Send(sendStr);
                Console.WriteLine($"send:{sendStr}");
                Thread.Sleep(1000);
            }
        }
    }
}
