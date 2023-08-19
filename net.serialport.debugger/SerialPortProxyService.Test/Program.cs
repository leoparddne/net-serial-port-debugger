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
            //Task.Run(() =>
            //{
            //    StartServer("127.0.0.1", 5000);
            //});

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



            SocketHelper acceptSocketHelper = null;


            while (true)
            {
                var acceptSocket = socketHelper.Accept();
                acceptSocketHelper = new SocketHelper(encode, acceptSocket);

                Task.Run(() =>
                {
                    while (acceptSocketHelper.ISConnect)
                    {
                        Receive(encode, acceptSocketHelper);
                    }
                });
            }
        }

        private static void Receive(Encoding encode, SocketHelper acceptSocketHelper)
        {
            if (!acceptSocketHelper.ISConnect)
            {
                return;
            }
            var buffer = new byte[4096];
            var size = acceptSocketHelper.Receive(buffer);
            if (size <= 0)
            {
                return;
            }

            var str = encode.GetString(buffer);
            Console.WriteLine($"receive:{str}");
        }

        static void StartClient(string ip, int port)
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var encode = System.Text.Encoding.GetEncoding("GB2312");

            SocketHelper socketHelper = new SocketHelper(encode);
            socketHelper.Connect(ip, port);

            while (true)
            {
                //send
                var sendStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var size = socketHelper.Send(sendStr);
                Console.WriteLine($"send:{sendStr}");
                Thread.Sleep(1000);


                //receive
                Receive(encode, socketHelper);
            }
        }
    }
}
