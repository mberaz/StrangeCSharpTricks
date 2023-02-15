namespace StrangeCSharpTricks.Excel;

public class WorksheetExportModel
{
    public List<string> ColumnHeaders { get; set; }
    public string WorksheetName { get; set; }
    public bool RightToLeft { get; set; } = true;
    public ColumnNamesSource ColumnNamesSource { get; set; }
    public Dictionary<string, string> Resources { get; set; }
}