using SerialPortProxyService.Common.Constant;
using SerialPortProxyService.Common.Helper;
using SerialPortProxyService.Common.Model;

namespace SerialPortProxyService.Common
{
    public class NetProxy : IProxyBase
    {
        private NetProxyConfig netProxyConfig;
        private SocketHelper acceptSocketHelper;

        public Action<byte[]> Recive { get; set; }
        public Action<byte[]> Send { get; set; }

        public RunningModeEnum RunningMode { get; set; }

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

        }

        public void Stop()
        {
            acceptSocketHelper.Close();
            acceptSocketHelper.Dispose();
        }
    }
}
