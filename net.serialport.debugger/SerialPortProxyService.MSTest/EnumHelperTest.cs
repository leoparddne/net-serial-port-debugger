using Newtonsoft.Json;
using SerialPortProxyService.Common.Helper;
using System.IO.Ports;

namespace SerialPortProxyService.MSTest
{
    [TestClass]
    public class EnumHelperTest
    {
        [TestMethod]
        public void EnumInfo()
        {
            var enumInfo = EnumHelper.GetEnumInfo(typeof(Parity));
            Console.WriteLine(JsonConvert.SerializeObject(enumInfo));
        }
    }
}