using SerialPortProxyService.Common.Constant;
using SerialPortProxyService.Common.Helper;
using SerialPortProxyService.Common.Model;

namespace SerialPortProxyService.Common
{
    public class SerialPortProxy : IProxyBase
    {
        private SerialPortProxyConfig serialPortProxyConfig;
        private SerialPortHelper serialPortHelper;
        public RunningModeEnum RunningMode { get; set; }


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
            if (serialPortProxyConfig == null)
            {
                throw new Exception("serialport config is null");
            }
            serialPortHelper = new SerialPortHelper(serialPortProxyConfig.Encode,
               serialPortProxyConfig.PortName,
               serialPortProxyConfig.BaudRate,
               serialPortProxyConfig.Parity,
               serialPortProxyConfig.DataBits,
               serialPortProxyConfig.StopBits);
            serialPortHelper.Open();
        }

        public void Stop()
        {
            serialPortHelper.Close();
            serialPortHelper.Dispose();
        }

        public void ChangeMode(RunningModeEnum mode)
        {
            this.RunningMode = mode;
        }
    }
}
