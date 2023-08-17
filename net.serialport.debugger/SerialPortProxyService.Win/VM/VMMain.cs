﻿using PropertyChanged;
using SerialPortProxyService.Win.Helper;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Text;

namespace SerialPortProxyService.Win.VM
{
    [AddINotifyPropertyChangedInterface]
    public class VMMain
    {
        private SerialPortHelper SerialPortHelper;

        public ObservableCollection<string> SerialPortList { get; set; }

        /// <summary>
        /// 选中的串口
        /// </summary>
        public string SelectSerialPort { get; set; }

        public VMMain()
        {
            var sysSerialPortList = SerialPort.GetPortNames();
            if (sysSerialPortList != null)
            {
                SerialPortList = new(sysSerialPortList);
            }
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var encode=System.Text.Encoding.GetEncoding("GB2312");
            SerialPortHelper = new SerialPortHelper(encode,"COM2", 9600, Parity.None, 8, StopBits.One);
            SerialPortHelper.DataReceivedEvent += SerialPortHelper_DataReceivedEvent;
            SerialPortHelper.Open();
        }

        private void SerialPortHelper_DataReceivedEvent(byte[] byteData)
        {
            var str = System.Text.Encoding.GetEncoding("GB2312").GetString(byteData, 0, byteData.Length);
            //var str = Encoding.UTF8.GetString(byteData, 0, byteData.Length);
            File.WriteAllText("./log.txt", str.Trim());
        }
    }
}
