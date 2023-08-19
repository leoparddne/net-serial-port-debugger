using SerialPortProxyService.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortProxyService.Common
{
    public interface IProxyBase
    {
        void Start();

        void Build(IProxyConfig config);
    }
}
