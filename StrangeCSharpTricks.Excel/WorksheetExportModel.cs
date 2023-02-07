namespace StrangeCSharpTricks.Excel
{
    public class WorksheetExportModel<T>
    {
        public WorksheetExportModel(List<T> model, List<string> columnHeaders, string worksheetName, bool rightToLeft = true)
        {
            Model = model;
            ColumnHeaders = columnHeaders;
            WorksheetName = worksheetName;
            RightToLeft = rightToLeft;
            UserAttributeForColumnHeaders = false;
        }

        public WorksheetExportModel(List<T> model, string worksheetName, bool rightToLeft = true)
        {
            Model = model;
            UserAttributeForColumnHeaders = true;
            WorksheetName = worksheetName;
            RightToLeft = rightToLeft;
        }

        public List<T> Model { get; set; }
        public List<string> ColumnHeaders { get; set; }
        public string WorksheetName { get; set; }
        public bool RightToLeft { get; set; } = true;
        public bool UserAttributeForColumnHeaders { get; set; }
    }
}
