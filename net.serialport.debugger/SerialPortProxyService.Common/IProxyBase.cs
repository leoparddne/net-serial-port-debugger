using SerialPortProxyService.Common.Constant;
using SerialPortProxyService.Common.Model;

namespace SerialPortProxyService.Common
{
    public interface IProxyBase
    {
        public RunningModeEnum RunningMode { get; set; }

        Action<byte[]> Receive { get; set; }

        /// <summary>
        /// 发送数据
        /// </summary>
        void Send(byte[] data);
        void Start();
        void Stop();

        void Build(IProxyConfig config);
    }
}
