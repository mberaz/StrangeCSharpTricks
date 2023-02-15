using Microsoft.AspNetCore.Mvc;

namespace StrangeCSharpTricks.DictionaryIsTheNewIf.Model
{
    public static class ContentHelper
    {
        public const string ExcelFileMemeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public static FileContentResult ToXlsxFile(byte[] bytes, string fileName)
        {
            return new FileContentResult(bytes, ExcelFileMemeType)
            {
                FileDownloadName = $"{fileName}.xlsx"
            };
        }
    }
}
