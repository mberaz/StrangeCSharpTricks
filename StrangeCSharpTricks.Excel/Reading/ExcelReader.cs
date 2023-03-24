using OfficeOpenXml;
using System.Reflection;

namespace StrangeCSharpTricks.Excel.Reading
{
    public static class ExcelReader
    {
        public static List<T> ReadExcel<T>(string filePath, int initialRow = 2) where T : class, new()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(filePath);
            var sheet = package.Workbook.Worksheets.FirstOrDefault();

            var list = new List<T>();

            var properties = GetProperties<T>();

            for (var i = initialRow; i < sheet.Dimension.End.Row + 1; i++)
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


        private static List<PropertyInfoModel> GetProperties<T>()
        {
            var list = new List<PropertyInfoModel>();
            foreach (var property in typeof(T).GetProperties())
            {
                var propertyColumnAttribute = property.GetCustomAttribute<PropertyColumnAttribute>();
                if (propertyColumnAttribute != null)
                {
                    var isNullable = property.PropertyType.IsGenericType &&
                                     property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

                    list.Add(new PropertyInfoModel
                    {
                        PropertyInfo = property,
                        Name = property.Name,
                        Column = propertyColumnAttribute.ColumnName,
                        IsNullable = isNullable,
                        Type = isNullable ? property.PropertyType.GetGenericArguments()[0] : property.PropertyType
                    });
                }
            }

            return list;
        }
    }
}

