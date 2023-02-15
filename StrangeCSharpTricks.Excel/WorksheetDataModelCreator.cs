namespace StrangeCSharpTricks.Excel
{
    public class WorksheetDataModelCreator
    {
        public static WorksheetDataModel CreateWorksheetDataModel<T>(List<T> model,
             string worksheetName, List<string> columnHeaders, bool rightToLeft = false)
        {
            return ExportToExcel.CreateWorksheetDataModel(model, new WorksheetExportModel
            {
                ColumnNamesSource = ColumnNamesSource.FromList,
                ColumnHeaders = columnHeaders,
                WorksheetName = worksheetName,
                RightToLeft = rightToLeft,
            });
        }

        public static WorksheetDataModel CreateWorksheetDataModel<T>(List<T> model, string worksheetName, bool rightToLeft = false)
        {
            return ExportToExcel.CreateWorksheetDataModel(model, new WorksheetExportModel
            {
                ColumnNamesSource = ColumnNamesSource.FromNameAttribute,
                WorksheetName = worksheetName,
                RightToLeft = rightToLeft,
            });
        }

        public static WorksheetDataModel CreateWorksheetDataModel<T>(List<T> model, string worksheetName, Dictionary<string, string> resources, bool rightToLeft = false)
        {
            return ExportToExcel.CreateWorksheetDataModel(model, new WorksheetExportModel
            {
                ColumnNamesSource = ColumnNamesSource.FromKeyAttribute,
                WorksheetName = worksheetName,
                RightToLeft = rightToLeft,
                Resources = resources,
            });
        }
    }
}
