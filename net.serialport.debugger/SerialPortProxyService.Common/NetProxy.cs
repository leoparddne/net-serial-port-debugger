using SerialPortProxyService.Common.Constant;
using SerialPortProxyService.Common.Helper;
using SerialPortProxyService.Common.Model;
using System.Diagnostics;
using System.Text;

namespace SerialPortProxyService.Common
{
    public class NetProxy : IProxyBase
    {
        private NetProxyConfig netProxyConfig;
        private SocketHelper acceptSocketHelper;
        private Task task;
        private CancellationTokenSource cancellationToken = new();
        private SocketHelper clientSocketHelper = null;

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
                case RunningModeEnum.Client:
                    task = Task.Run(() =>
                    {
                        StartClient(netProxyConfig.IP, netProxyConfig.Port);
                    }, cancellationToken.Token);
                    break;
                case RunningModeEnum.Server:
                    task = Task.Run(() =>
                    {
                        StartServer("0.0.0.0", netProxyConfig.Port);
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

            acceptSocketHelper = new SocketHelper(netProxyConfig.Encode);
            acceptSocketHelper.Bind(ip, port);

            acceptSocketHelper.Listen();

            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                try
                {
                    var clientSocket = acceptSocketHelper.Accept();
                    clientSocketHelper = new SocketHelper(netProxyConfig.Encode, clientSocket);

                    ReceiveSocketData(clientSocketHelper);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                }
            }
        }

        private void ReceiveSocketData(SocketHelper acceptSocketHelper)
        {
            while (acceptSocketHelper.ISConnect)
            {
                var buffer = new byte[4096];
                int size = 0;

                try
                {
                    size = acceptSocketHelper.Receive(buffer);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                }
                if (size <= 0)
                {
                    continue;
                }

                var finalBuffer = new byte[size];

                buffer.ToList().CopyTo(0, finalBuffer, 0, size);

                try
                {
                    Receive(finalBuffer);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                }
            }
        }


        private void StartClient(string ip, int port)
        {
            clientSocketHelper = new SocketHelper(netProxyConfig.Encode);
            clientSocketHelper.Connect(ip, port);
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                ReceiveSocketData(clientSocketHelper);
            }
        }

        public void Stop()
        {
            cancellationToken.Cancel();
            acceptSocketHelper.Close();
            acceptSocketHelper.Dispose();

            clientSocketHelper?.Close();
            clientSocketHelper?.Dispose();
        }

        public void Send(byte[] data)
        {
            clientSocketHelper.Send(data);
        }
    }
}
