namespace StrangeCSharpTricks.Excel;

public class WorksheetDataModel
{
    public Dictionary<string, string> HeaderMap { get; set; }
    public List<Dictionary<string, ExcelCellModel>> DataRows { get; set; }
    public string WorksheetName { get; set; }
    public bool RightToLeft { get; set; } = true;
}