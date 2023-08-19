using SerialPortProxyService.Common.Constant;
using SerialPortProxyService.Common.Helper;
using SerialPortProxyService.Common.Model;

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
