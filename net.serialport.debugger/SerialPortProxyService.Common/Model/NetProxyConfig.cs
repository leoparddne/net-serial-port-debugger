﻿using System.Diagnostics.CodeAnalysis;

namespace SerialPortProxyService.Common.Model
{
    public class NetProxyConfig : IProxyConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MemberNotNull(nameof(Port))]
        public uint Port { get; set; }
    }
}
