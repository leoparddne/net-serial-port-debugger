using SerialPortProxyService.Common.Model;

namespace SerialPortProxyService.Common
{
    public interface IProxyBase
    {
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
