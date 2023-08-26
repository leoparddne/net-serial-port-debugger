using SerialPortProxyService;
using SerialPortProxyService.Common;
using SerialPortProxyService.Common.Constant;
using SerialPortProxyService.Common.Model;
using System.Text;

ConfigurationManager manager = new ConfigurationManager();
manager.AddJsonFile("appsettings.json");

var config = new ServiceConfig();
manager.Bind(config);

if (config.Net == null || config.Serial == null)
{
    throw new Exception("ÅäÖÃÒì³£");
}

var proxyAgent = new ProxyAgent();
Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
var encode = System.Text.Encoding.GetEncoding("GB2312");

proxyAgent.Build(new ProxyAgentConfig(

     new NetProxyConfig
     {
         Encode = encode,
         IP = config.Net.IP,
         Port = config.Net.Port
     },
     new SerialPortProxyConfig
     {
         Encode = encode,
         PortName = config.Serial.PortName,
         BaudRate = config.Serial.BaudRate,
         Parity = config.Serial.Parity,
         DataBits = config.Serial.DataBits,
         StopBits = config.Serial.StopBits
     })
);

switch (config.Mode)
{
    case RunningModeEnum.Client:
        proxyAgent.SwitchMode(RunningModeEnum.Client);
        break;
    case RunningModeEnum.Server:
        proxyAgent.SwitchMode(RunningModeEnum.Server);
        break;
    default:
        throw new Exception("Running mode error");
        break;
}


proxyAgent.Start();


var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        //services.AddHostedService<Worker>();
    });
if (System.OperatingSystem.IsWindows())
{
    hostBuilder.UseWindowsService();
}
if (System.OperatingSystem.IsLinux())
{
    hostBuilder.UseSystemd();
}


IHost host =
    hostBuilder.Build();
host.Run();
