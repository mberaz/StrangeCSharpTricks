using System.Reflection;

namespace StrangeCSharpTricks.Excel.Reading
{
    public class PropertyInfoModel
    {
        public PropertyInfo PropertyInfo { get; set; }
        public string Name { get; set; }
        public string Column { get; set; }
        public Type Type { get; set; }
        public bool IsNullable { get; set; }
    }
}
