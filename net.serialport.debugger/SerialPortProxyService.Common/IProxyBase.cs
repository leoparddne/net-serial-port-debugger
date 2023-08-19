using SerialPortProxyService.Common.Constant;
using SerialPortProxyService.Common.Model;

namespace SerialPortProxyService.Common
{
    public interface IProxyBase
    {
        RunningModeEnum RunningMode { get; set; }
        void Start();
        void Stop();

        void Build(IProxyConfig config);

        public void ChangeMode(RunningModeEnum mode);
    }
}
