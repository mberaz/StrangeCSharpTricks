using OfficeOpenXml;
using System.Reflection;

namespace StrangeCSharpTricks.Excel.Reading
{
    public static class ExcelReader
    {
        public static List<T> ReadExcel<T>(string filePath, string worksheetName = null, int initialDataRow = 2) where T : class, new()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(filePath);
            var sheet = worksheetName == null ?
                package.Workbook.Worksheets.FirstOrDefault() :
                package.Workbook.Worksheets.FirstOrDefault(s => s.Name == worksheetName);

            var list = new List<T>();

            var properties = GetProperties<T>();

            for (var i = initialDataRow; i < sheet.Dimension.End.Row + 1; i++)
            {
                var item = new T();

                foreach (var property in properties)
                {
                    var value = sheet.Cells[$"{property.Column}{i}"].Value;

                    property.PropertyInfo.SetValue(item,
                        value != null ?
                            Convert.ChangeType(value, property.Type) :
                            property.IsNullable ? null : default);
                }

                list.Add(item);
            }

            return list;
        }

        public static List<T> ReadExcelUsingTitles<T>(string filePath, string worksheetName = null,
            Func<string, string, bool> titleCompareDelegate = null,
        int titleRow = 1, int initialDataRow = 2) where T : class, new()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(filePath);
            var sheet = worksheetName == null ?
                package.Workbook.Worksheets.FirstOrDefault() :
                package.Workbook.Worksheets.FirstOrDefault(s => s.Name == worksheetName);

            titleCompareDelegate ??= (a, b) => a.Trim() == b.Trim();

            var list = new List<T>();

            var properties = GetProperties<T>();

            var propertyToColumnMapping = new Dictionary<int, PropertyInfoModel>();
            //titles
            for (var col = 1; col < sheet.Dimension.End.Column + 1; col++)
            {
                var title = sheet.Cells[titleRow, col].Value?.ToString();
                if (title != null)
                {
                    var property = properties.FirstOrDefault(s => titleCompareDelegate(s.Column, title));
                    if (property != null)
                    {
                        propertyToColumnMapping.Add(col, property);
                    }
                }
            }

            //data
            for (var row = initialDataRow; row < sheet.Dimension.End.Row + 1; row++)
            {
                var item = new T();
                foreach (var (col, property) in propertyToColumnMapping)
                {
                    var value = sheet.Cells[row, col].Value;

                    property.PropertyInfo.SetValue(item,
                           value != null ?
                                 Convert.ChangeType(value, property.Type) :
                                 property.IsNullable ? null : default);
                }

                list.Add(item);
            }

            return list;
        }


        private static List<PropertyInfoModel> GetProperties<T>()
        {
            var list = new List<PropertyInfoModel>();
            foreach (var property in typeof(T).GetProperties())
            {
                var propertyColumnAttribute = property.GetCustomAttribute<PropertyColumnAttribute>();
                var propertyTitleNameAttribute = property.GetCustomAttribute<PropertyTitleNameAttribute>();
                if (propertyColumnAttribute != null || propertyTitleNameAttribute != null)
                {
                    var isNullable = property.PropertyType.IsGenericType &&
                                     property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

                    list.Add(new PropertyInfoModel
                    {
                        PropertyInfo = property,
                        Name = property.Name,
                        Column = propertyColumnAttribute?.ColumnName ?? propertyTitleNameAttribute.ColumnTitle,
                        IsNullable = isNullable,
                        Type = isNullable ? property.PropertyType.GetGenericArguments()[0] : property.PropertyType
                    });
                }
            }

            return list;
        }
    }
}

