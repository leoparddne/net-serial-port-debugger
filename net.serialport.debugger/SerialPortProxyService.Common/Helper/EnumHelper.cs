namespace SerialPortProxyService.Common.Helper
{
    public static class EnumHelper
    {
        public static List<string> GetEnumNames(this Enum value)
        {
            var type = value.GetType();
            var names = Enum.GetNames(type).ToList();
            return names;
        }

        public static Dictionary<string, int> GetEnumInfo(Type enumType)
        {
            var values = Enum.GetValues(enumType);

            var result = new Dictionary<string, int>();

            foreach (var valueItem in values)
            {
                var name = Enum.GetName(enumType, valueItem);
                result.Add(name, (int)valueItem);
            }

            return result;
        }
    }
}
