namespace StrangeCSharpTricks.Excel
{
    public enum ColumnNamesSource
    {
        FromNameAttribute,
        FromKeyAttribute,
        FromList
    }
    public class WorksheetExportModel<T>
    {
        public WorksheetExportModel(List<T> model, List<string> columnHeaders, string worksheetName, bool rightToLeft = true)
        {
            Model = model;
            ColumnHeaders = columnHeaders;
            WorksheetName = worksheetName;
            RightToLeft = rightToLeft;
            ColumnNamesSource = ColumnNamesSource.FromList;
        }

        public WorksheetExportModel(List<T> model, string worksheetName, bool rightToLeft = true)
        {
            Model = model;
            ColumnNamesSource = ColumnNamesSource.FromNameAttribute;
            WorksheetName = worksheetName;
            RightToLeft = rightToLeft;
        }

        public WorksheetExportModel(List<T> model, string worksheetName, Dictionary<string, string> resources, bool rightToLeft = true)
        {
            Model = model;
            ColumnNamesSource = ColumnNamesSource.FromKeyAttribute;
            WorksheetName = worksheetName;
            RightToLeft = rightToLeft;
            Resources = resources;
        }

        public List<T> Model { get; set; }
        public List<string> ColumnHeaders { get; set; }
        public string WorksheetName { get; set; }
        public bool RightToLeft { get; set; } = true;
        public ColumnNamesSource ColumnNamesSource { get; set; }

        public Dictionary<string, string> Resources { get; set; }
    }
}
