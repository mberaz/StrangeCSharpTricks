namespace StrangeCSharpTricks.Excel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ExcelColumnIgnoreAttribute : Attribute
    {
        public ExcelColumnIgnoreAttribute()
        {

        }
    }
}
