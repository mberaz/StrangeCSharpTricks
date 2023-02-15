namespace StrangeCSharpTricks.Excel;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ExcelColumnResourceKeyAttribute : Attribute
{
    public string ColumnKey;

    public ExcelColumnResourceKeyAttribute(string columnKey)
    {
        ColumnKey = columnKey;
    }
}