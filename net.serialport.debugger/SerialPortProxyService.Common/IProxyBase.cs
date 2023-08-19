using SerialPortProxyService.Common.Constant;
using SerialPortProxyService.Common.Model;

namespace SerialPortProxyService.Common
{
    public interface IProxyBase
    {
        /// <summary>
        /// 接收数据时回掉
        /// </summary>
        Action<byte[]> Recive { get; set; }

        /// <summary>
        /// 发送数据时回掉
        /// </summary>
        Action<byte[]> Send { get; set; }
        void Start();
        void Stop();

        void Build(IProxyConfig config);
    }
}
