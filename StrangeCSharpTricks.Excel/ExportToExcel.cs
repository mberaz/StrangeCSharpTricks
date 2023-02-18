using OfficeOpenXml;

namespace StrangeCSharpTricks.Excel
{
    public static class ExportToExcel
    {
        public static byte[] ExportWorksheets(WorksheetDataModel worksheet)
        {
            return ExportWorksheets(new List<WorksheetDataModel> { worksheet });
        }

        public static byte[] ExportWorksheets(List<WorksheetDataModel> worksheetList)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage();

            foreach (var worksheet in worksheetList)
            {
                var ws = package.Workbook.Worksheets.Add(worksheet.WorksheetName);
                ws.View.RightToLeft = worksheet.RightToLeft;

                const int titleRowsOffset = 0;

                //column headers
                var colIndex = 0;
                foreach (var x in worksheet.HeaderMap)
                {
                    ws.SetValue(titleRowsOffset + 1, colIndex + 1, x.Value);
                    ws.Cells[titleRowsOffset + 1, colIndex + 1].Style.Font.Bold = true;
                    var col = ws.Column(colIndex + 1);
                    col.AutoFit(MinimumWidth: 15);

                    colIndex++;
                }

                //data
                for (var row = 1; row <= worksheet.DataRows.Count; row++)
                {
                    var currentRow = worksheet.DataRows[row - 1];
                    var cell = 1;

                    foreach (var header in worksheet.HeaderMap)
                    {
                        var isExistInDictionary = currentRow.ContainsKey(header.Value);
                        var currentValue = currentRow[header.Value];

                        ws.SetValue(titleRowsOffset + row + 1, cell, isExistInDictionary ? currentValue.Value : null);
                        using (var excelRange = ws.Cells[titleRowsOffset + row + 1, cell])
                        {
                            if (isExistInDictionary)
                            {
                                excelRange.Style.Numberformat.Format = GetStringFormat(currentValue.Value);
                            }
                        }
                        cell++;
                    }
                }
            }

            return package.GetAsByteArray();
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

    }
}