using SerialPortProxyService.Common.Model;

namespace SerialPortProxyService.Common
{
    public class SerialPortProxy : IProxyBase
    {
        private SerialPortProxyConfig serialPortProxyConfig;

        public void Build(IProxyConfig config)
        {
            if (config is not SerialPortProxyConfig serialPortConfig)
            {
                throw new Exception("serialport error config");
            }

            serialPortProxyConfig = serialPortConfig;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
