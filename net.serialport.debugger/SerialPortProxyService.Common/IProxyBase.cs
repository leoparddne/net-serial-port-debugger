using SerialPortProxyService.Common.Model;

namespace SerialPortProxyService.Common
{
    public interface IProxyBase
    {
        void Start();
        void Stop();

        void Build(IProxyConfig config);
    }
}
