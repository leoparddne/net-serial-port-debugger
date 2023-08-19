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
        private Task task;
        private CancellationTokenSource cancellationToken = new();

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
                    task = Task.Run(() =>
                   {
                       StartClient(netProxyConfig.IP, netProxyConfig.Port);
                   }, cancellationToken.Token);
                    break;
                case RunningModeEnum.Net:
                    task = Task.Run(() =>
                   {
                       StartServer(netProxyConfig.IP, netProxyConfig.Port);
                   }, cancellationToken.Token);
                    break;
                default:
                    throw new Exception("error running mode");
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

                var finalBuffer = new byte[size];

                buffer.ToList().CopyTo(0, finalBuffer, 0, size);

                var str = encode.GetString(finalBuffer);
                Console.WriteLine($"receive:{str}");
                Receive(finalBuffer);
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
            cancellationToken.Cancel();
            acceptSocketHelper.Close();
            acceptSocketHelper.Dispose();
        }

        public void Send(byte[] data)
        {
            acceptSocketHelper.Send(data);
        }
    }
}
