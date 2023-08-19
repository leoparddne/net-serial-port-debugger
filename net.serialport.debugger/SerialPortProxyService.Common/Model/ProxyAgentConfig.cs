namespace SerialPortProxyService.Common.Model
{
    public class ProxyAgentConfig : IProxyConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public NetProxyConfig NetConfig { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SerialPortProxyConfig SerialConfig { get; set; }


        public ProxyAgentConfig(NetProxyConfig netConfig, SerialPortProxyConfig serialConfig)
        {
            NetConfig = netConfig;
            SerialConfig = serialConfig;
        }
    }
}
