using System.Reflection;

namespace StrangeCSharpTricks.Excel
{
    public class WorksheetDataModelCreator
    {
        public static WorksheetDataModel CreateWorksheetDataModel<T>(List<T> model,
             string worksheetName, List<string> columnHeaders, bool rightToLeft = false)
        {
            return CreateWorksheetDataModel(model, new WorksheetExportModel
            {
                ColumnNamesSource = ColumnNamesSource.FromList,
                ColumnHeaders = columnHeaders,
                WorksheetName = worksheetName,
                RightToLeft = rightToLeft,
            });
        }

        public static WorksheetDataModel CreateWorksheetDataModel<T>(List<T> model, 
            string worksheetName, bool rightToLeft = false)
        {
            return CreateWorksheetDataModel(model, new WorksheetExportModel
            {
                ColumnNamesSource = ColumnNamesSource.FromNameAttribute,
                WorksheetName = worksheetName,
                RightToLeft = rightToLeft,
            });
        }

        public static WorksheetDataModel CreateWorksheetDataModel<T>(List<T> model, 
            string worksheetName, Dictionary<string, string> resources, bool rightToLeft = false)
        {
            return CreateWorksheetDataModel(model, new WorksheetExportModel
            {
                ColumnNamesSource = ColumnNamesSource.FromKeyAttribute,
                WorksheetName = worksheetName,
                RightToLeft = rightToLeft,
                Resources = resources,
            });
        }



        private static WorksheetDataModel CreateWorksheetDataModel<T>(List<T> model, WorksheetExportModel worksheet)
        {
            var headerMap = worksheet.ColumnNamesSource switch
            {
                ColumnNamesSource.FromNameAttribute => GetHeaderMapFromAttributes<T>(),
                ColumnNamesSource.FromKeyAttribute => GetResourceHeaderMapFromAttributes<T>(worksheet.Resources),
                _ => GetHeaderMap(worksheet.ColumnHeaders, GetModelPropertiesNames<T>())
            };

            var dataRows = GetExcelDictionary(model, headerMap);

            return new WorksheetDataModel
            {
                RightToLeft = worksheet.RightToLeft,
                WorksheetName = worksheet.WorksheetName,
                DataRows = dataRows,
                HeaderMap = headerMap
            };
        }

        private static List<Dictionary<string, ExcelCellModel>> GetExcelDictionary<T>(List<T> list, Dictionary<string, string> headerMap)
        {
            var dict = new List<Dictionary<string, ExcelCellModel>>();
            foreach (var item in list)
            {
                var row = new Dictionary<string, ExcelCellModel>();

                foreach (var prop in typeof(T).GetProperties())
                {
                    if (headerMap.ContainsKey(prop.Name))
                    {
                        row.Add(headerMap[prop.Name], new ExcelCellModel
                        {
                            Type = prop.PropertyType,
                            Value = prop.GetValue(item) ?? null
                        });
                    }
                }
                dict.Add(row);
            }
            return dict;
        }

        private static Dictionary<string, string> GetHeaderMap(List<string> columnHeaders, List<string> columnNames)
        {
            if (columnHeaders.Count != columnNames.Count)
            {
                throw new Exception("the columnNames and columnHeaders lists must have the same length");
            }

            var headerMap = new Dictionary<string, string>();

            for (var i = 0; i < columnNames.Count; i++)
            {
                headerMap.Add(columnNames[i], columnHeaders[i]);
            }

            return headerMap;

        }

        private static Dictionary<string, string> GetHeaderMapFromAttributes<T>()
        {
            var columnNames = new Dictionary<string, string>();

            foreach (var property in typeof(T).GetProperties())
            {
                if (property.GetCustomAttribute<ExcelColumnIgnoreAttribute>() == null)
                {
                    var columnNameAttribute = property.GetCustomAttribute<ExcelColumnNameAttribute>();
                    if (columnNameAttribute != null)
                    {
                        columnNames.Add(property.Name, columnNameAttribute.ColumnName);
                    }
                }
            }

            return columnNames;
        }

        private static List<string> GetModelPropertiesNames<T>()
        {
            return typeof(T).GetProperties().Select(p => p.Name).ToList();
        }

        private static Dictionary<string, string> GetResourceHeaderMapFromAttributes<T>(Dictionary<string, string> resources)
        {
            var columnNames = new Dictionary<string, string>();

            foreach (var property in typeof(T).GetProperties())
            {
                if (property.GetCustomAttribute<ExcelColumnIgnoreAttribute>() == null)
                {
                    var resourceKeyAttribute = property.GetCustomAttribute<ExcelColumnResourceKeyAttribute>();
                    if (resourceKeyAttribute != null)
                    {
                        columnNames.Add(property.Name, resources[resourceKeyAttribute.ColumnKey]);
                    }
                }
            }

            return columnNames;
        }
    }
}
