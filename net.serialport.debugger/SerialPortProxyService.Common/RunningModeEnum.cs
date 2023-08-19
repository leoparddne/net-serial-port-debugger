using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortProxyService.Common
{
    public enum RunningModeEnum
    {
        /// <summary>
        /// serial port => net
        /// </summary>
        SerialPort,

        /// <summary>
        /// net => serial port
        /// </summary>
        Net
    }
}
