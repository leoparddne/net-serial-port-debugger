using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortProxyService.Win.Helper
{
    public class SerialPortHelper
    {
        private SerialPort SerialPort { get; set; }

        public SerialPortHelper()
        {
            SerialPort = new SerialPort();
        }
    }
}
