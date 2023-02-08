using OfficeOpenXml;
using System.Reflection;

namespace StrangeCSharpTricks.Excel
{
    public static class ExportToExcel
    {
        public static byte[] ExportWorksheets<T>(WorksheetExportModel<T> worksheet)
        {
            return ExportWorksheets<T>(new List<WorksheetExportModel<T>> { worksheet });
        }
        public static byte[] ExportWorksheets<T>(List<WorksheetExportModel<T>> worksheetList)
        {
            var package = new ExcelPackage();

            foreach (var worksheet in worksheetList)
            {
                var headerMap = worksheet.ColumnNamesSource switch
                {
                    ColumnNamesSource.FromNameAttribute => GetHeaderMapFromAttributes<T>(),
                    ColumnNamesSource.FromKeyAttribute => GetResourceHeaderMapFromAttributes<T>(worksheet.Resources),
                    _ => GetHeaderMap(worksheet.ColumnHeaders, GetModelPropertiesNames<T>())
                };

                var dataRows = GetExcelDictionary(worksheet.Model, headerMap);

                var ws = package.Workbook.Worksheets.Add(worksheet.WorksheetName);
                ws.View.RightToLeft = worksheet.RightToLeft;

                const int titleRowsOffset = 0;

                //column headers
                var colIndex = 0;
                foreach (var x in headerMap)
                {
                    ws.SetValue(titleRowsOffset + 1, colIndex + 1, x.Value);
                    ws.Cells[titleRowsOffset + 1, colIndex + 1].Style.Font.Bold = true;
                    var col = ws.Column(colIndex + 1);
                    col.AutoFit(15);

                    colIndex++;
                }

                //data
                for (var row = 1; row <= dataRows.Count; row++)
                {
                    var currentRow = dataRows[row - 1];
                    var cell = 1;

                    foreach (var header in headerMap)
                    {
                        var isExistInDictionary = currentRow.Keys.Contains(header.Value);

                        ws.SetValue(titleRowsOffset + row + 1, cell, isExistInDictionary ? currentRow[header.Value].Value2 : null);
                        using (var rng = ws.Cells[titleRowsOffset + row + 1, cell])
                        {
                            if (isExistInDictionary)
                            {
                                rng.Style.Numberformat.Format = GetStringFormat(currentRow[header.Value].Value2);
                            }
                        }
                        cell++;
                    }
                }
            }

            return package.GetAsByteArray();
        }

        private static List<Dictionary<string, ExcelCellModel>> GetExcelDictionary<T>(List<T> list, Dictionary<string, string> headerMap)
        {
            var dict = new List<Dictionary<string, ExcelCellModel>>();
            var objectType = typeof(T);
            var properties = objectType.GetProperties();
            foreach (var item in list)
            {
                var row = new Dictionary<string, ExcelCellModel>();

                foreach (var prop in properties)
                {
                    if (headerMap.ContainsKey(prop.Name))
                    {
                        var isDate = prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?);
                        row.Add(headerMap[prop.Name], new ExcelCellModel
                        {
                            Type = !isDate ? prop.PropertyType : typeof(DateTime?),
                            Value2 = prop.GetValue(item) == null ? null : prop.GetValue(item)
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

        private static string GetStringFormat(object obj)
        {
            if (obj != null)
            {
                switch (obj.GetType().ToString())
                {
                    case "System.float":
                        {
                            return "#,###,##0.00";
                        }
                    case "System.double":
                        {
                            return "#,###,##0.00";
                        }
                    case "System.float?":
                        {
                            return "#,###,##0.00";
                        }
                    case "System.double?":
                        {
                            return "#,###,##0.00";
                        }
                    case "System.DateTime":
                        {
                            return "dd/MM/yyyy";
                        }
                    case "System.DateTime?":
                        {
                            return "dd/MM/yyyy";
                        }
                    case "System.Int32":
                        {
                            return "#";
                        }
                    case "System.Int32?":
                        {
                            return "#";
                        }
                    default: { return ""; }
                }

            }
            return "";
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