namespace StrangeCSharpTricks.Excel.Reading
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PropertyColumnAttribute : Attribute
    {
        public string ColumnName;

        public PropertyColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
