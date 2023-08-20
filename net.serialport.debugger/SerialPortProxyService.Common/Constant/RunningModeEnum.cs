using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortProxyService.Common.Constant
{
    public enum RunningModeEnum
    {
        /// <summary>
        /// serial port => net
        /// </summary>
        Client,

        /// <summary>
        /// net => serial port
        /// </summary>
        Server
    }
}
