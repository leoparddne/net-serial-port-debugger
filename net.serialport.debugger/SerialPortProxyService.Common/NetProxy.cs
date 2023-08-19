using SerialPortProxyService.Common.Constant;
using SerialPortProxyService.Common.Helper;
using SerialPortProxyService.Common.Model;
using System.Text;

namespace SerialPortProxyService.Common
{
    public class NetProxy : IProxyBase
    {
        private NetProxyConfig netProxyConfig;
        private SocketHelper acceptSocketHelper;

        public RunningModeEnum RunningMode { get; set; }

        public Action<byte[]> Receive { get; set; }

        public NetProxy(Action<byte[]> receive)
        {
            Receive = receive;
        }

        public void Build(IProxyConfig config)
        {
            if (config is not NetProxyConfig netConfig)
            {
                throw new Exception("net error config");
            }

            netProxyConfig = netConfig;
        }

        public void Start()
        {
            if (netProxyConfig == null)
            {
                throw new Exception("net config is null");
            }
            acceptSocketHelper = new SocketHelper(netProxyConfig.Encode);
            acceptSocketHelper.ReceiveCallback = Receive;

            switch (RunningMode)
            {
                case RunningModeEnum.SerialPort:
                    StartServer(netProxyConfig.IP, netProxyConfig.Port);
                    break;
                case RunningModeEnum.Net:
                    StartClient(netProxyConfig.IP, netProxyConfig.Port);
                    break;
                default:
                    break;
            }
        }

        public void StartServer(string ip, int port)
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
                    ReceiveSocketData(encode, acceptSocketHelper);
                });
            }
        }

        private void ReceiveSocketData(Encoding encode, SocketHelper acceptSocketHelper)
        {
            while (acceptSocketHelper.ISConnect)
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


        private void StartClient(string ip, int port)
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

        public void Stop()
        {
            acceptSocketHelper.Close();
            acceptSocketHelper.Dispose();
        }

        public void Send(byte[] data)
        {
            acceptSocketHelper.Send(data);
        }
    }
}
