// See https://aka.ms/new-console-template for more information
using SerialPortProxyService.Common;
using SerialPortProxyService.Common.Constant;
using SerialPortProxyService.Common.Model;
using System.IO.Ports;
using System.Text;

Console.WriteLine("Hello, World!");


var proxy = new ProxyAgent();
Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
var encode = System.Text.Encoding.GetEncoding("GB2312");

proxy.Build(new ProxyAgentConfig(

     new NetProxyConfig
     {
         Encode = encode,
         IP = "127.0.0.1",
         Port = 5000
     },
     new SerialPortProxyConfig
     {
         Encode = encode,
         PortName = "COM2",
         BaudRate = 9600,
         Parity = Parity.None,
         DataBits = 8,
         StopBits = StopBits.One
     })
);

//proxy.SwitchMode(RunningModeEnum.Net);
proxy.SwitchMode(RunningModeEnum.Client);

proxy.Start();

Console.ReadLine();