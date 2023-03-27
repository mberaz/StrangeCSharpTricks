namespace StrangeCSharpTricks.Excel.Reading;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class PropertyTitleNameAttribute : Attribute
{
    public string ColumnTitle;

    public PropertyTitleNameAttribute(string columnTitle)
    {
        ColumnTitle = columnTitle;
    }
}