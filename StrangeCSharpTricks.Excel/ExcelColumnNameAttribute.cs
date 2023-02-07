namespace StrangeCSharpTricks.Excel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ExcelColumnNameAttribute : Attribute
    {
        public string ColumnName;

        public ExcelColumnNameAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
