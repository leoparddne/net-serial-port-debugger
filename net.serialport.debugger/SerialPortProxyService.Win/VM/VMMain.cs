using HandyControl.Controls;
using PropertyChanged;
using SerialPortProxyService.Common;
using SerialPortProxyService.Common.Constant;
using SerialPortProxyService.Common.Helper;
using SerialPortProxyService.Common.Model;
using SerialPortProxyService.Win.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SerialPortProxyService.Win.VM
{
    [AddINotifyPropertyChangedInterface]
    public class VMMain
    {
        private ProxyAgent proxyAgent;
        public string BtnDisplay { get; set; } = "开始监听";
        public bool ModifyEnable { get; set; } = true;
        private SerialPortHelper SerialPortHelper;

        public ObservableCollection<string> SerialPortList { get; set; }

        public Dictionary<string, int> ParityList { get; set; }

        public KeyValuePair<string, int> SelectParity { get; set; }

        public Dictionary<string, int> StopBitsList { get; set; }

        public KeyValuePair<string, int> SelectStopBits { get; set; }


        public string IP { get; set; } = "127.0.0.1";

        public int Port { get; set; } = 5000;

        /// <summary>
        /// 是否为客户端模式
        /// </summary>
        public bool ISClientMode { get; set; } = true;

        /// <summary>
        /// 选中的串口
        /// </summary>
        public string SelectSerialPort { get; set; }


        public int BaudRate { get; set; } = 9600;
        public Parity Parity { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }

        public ICommand OpenServiceCommand { get; set; }


        public VMMain()
        {
            var sysSerialPortList = SerialPort.GetPortNames();
            if (sysSerialPortList != null)
            {
                SerialPortList = new(sysSerialPortList);
            }

            ParityList = EnumHelper.GetEnumInfo(typeof(Parity));
            SelectParity = ParityList.First();
            StopBitsList = EnumHelper.GetEnumInfo(typeof(StopBits));
            SelectStopBits = StopBitsList.First();

            OpenServiceCommand = new BaseCommand((para) =>
            {
                if (string.IsNullOrEmpty(SelectSerialPort))
                {
                    Growl.Error("请选择串口");
                    return;
                }

                proxyAgent = new ProxyAgent();
                Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                var encode = System.Text.Encoding.GetEncoding("GB2312");

                proxyAgent.Build(new ProxyAgentConfig(

                     new NetProxyConfig
                     {
                         Encode = encode,
                         IP = IP,
                         Port = Port
                     },
                     new SerialPortProxyConfig
                     {
                         Encode = encode,
                         PortName = SelectSerialPort,
                         BaudRate = BaudRate,
                         Parity = (Parity)SelectParity.Value,
                         DataBits = DataBits,
                         StopBits = (StopBits)SelectStopBits.Value
                     })
                );

                if (ISClientMode)
                {
                    proxyAgent.SwitchMode(RunningModeEnum.Client);
                }
                else
                {
                    proxyAgent.SwitchMode(RunningModeEnum.Server);
                }

                if (ModifyEnable)
                {
                    proxyAgent.Start();
                }
                else
                {
                    proxyAgent.Stop();
                }

                //监听成功后不允许修改
                ModifyEnable = !ModifyEnable;
                BtnDisplay = ModifyEnable ? "开始监听" : "停止监听";
            });
        }
    }
}
