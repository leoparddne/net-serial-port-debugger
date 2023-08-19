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

        /// <summary>
        /// 构造核心参数
        /// </summary>
        public void Build()
        {
            //串口模式下需要本机串口及远程目标网络数据
            //网络模式下需要本机监听网络信息及本机目标串口信息
            //两种模式均需要对称的参数 - 即均需要串口、网络信息
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
