using SerialPortProxyService.Common.Constant;
using SerialPortProxyService.Common.Model;

namespace SerialPortProxyService.Common
{
    /// <summary>
    /// 统一代理
    /// </summary>
    public class ProxyAgent : IProxyBase
    {
        /// <summary>
        /// 网络代理
        /// </summary>
        private NetProxy netProxy;

        /// <summary>
        /// 串口代理
        /// </summary>
        private SerialPortProxy serialPortProxy;

        /// <summary>
        /// 运行模式
        /// </summary>
        public RunningModeEnum RunningMode { get; private set; }

        public void ChangeMode(RunningModeEnum mode)
        {
            this.RunningMode = mode;
        }

        public ProxyAgent()
        {
            netProxy = new();
            serialPortProxy = new();
        }

        /// <summary>
        /// 构造核心参数 - 类型指定为ProxyAgentConfig
        /// </summary>
        public void Build(IProxyConfig config)
        {
            if (config is not ProxyAgentConfig agentConfig)
            {
                throw new Exception("error config");
            }

            //串口模式下需要本机串口及远程目标网络数据
            //网络模式下需要本机监听网络信息及本机目标串口信息
            //两种模式均需要对称的参数 - 即均需要串口、网络信息
            netProxy.Build(agentConfig.NetConfig);
            serialPortProxy.Build(agentConfig.SerialConfig);
        }

        public void Start()
        {
            netProxy.Start();
            serialPortProxy.Start();
        }
    }
}
