using Microsoft.AspNetCore.Mvc;
using StrangeCSharpTricks.DictionaryIsTheNewIf.Model;
using StrangeCSharpTricks.DictionaryIsTheNewIf.Model.ExportModels;
using StrangeCSharpTricks.Excel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StrangeCSharpTricks.DictionaryIsTheNewIf.Controllers
{
    [Route("Export")]
    public class ExportController : Controller
    {
        [HttpGet("ClassWithAttributeName")]
        public async Task<IActionResult> ClassWithAttributeName()
        {
            var list = new List<ClassWithAttributeName>
            {
                new ClassWithAttributeName{Email = "a.b@mail.com",FirstName = "a", LastName = "b",Id = 1},
                new ClassWithAttributeName{Email = "c.d@mail.com",FirstName = "c", LastName = "d",Id = 2},
                new ClassWithAttributeName{Email = "e.f@mail.com",FirstName = "e", LastName = "f",Id = 2},
            };

            var bytes = ExportToExcel.ExportWorksheets(WorksheetDataModelCreator.CreateWorksheetDataModel(list, "ClassWithAttributeName"));
            return ContentHelper.ToXlsxFile(bytes, "ClassWithAttributeName");
        }

        [HttpGet("ClassWithAttributeKey")]
        public async Task<IActionResult> ClassWithAttributeKey()
        {
            var list = new List<ClassWithAttributeKey>
            {
                new ClassWithAttributeKey{Email = "a.b@mail.com",FirstName = "a", LastName = "b",Id = 1},
                new ClassWithAttributeKey{Email = "c.d@mail.com",FirstName = "c", LastName = "d",Id = 2},
                new ClassWithAttributeKey{Email = "e.f@mail.com",FirstName = "e", LastName = "f",Id = 2},
            };

            //get resources from a data base
            var resources = new Dictionary<string, string>
            {
                {"IdKey","id"},
                {"FirstNameKey","first_name"},
                {"LastNameKey","last_name"},
                {"EmailKey","email"}
            };

            var bytes = ExportToExcel.ExportWorksheets(WorksheetDataModelCreator.CreateWorksheetDataModel(list, "ClassWithAttributeKey", resources));
            return ContentHelper.ToXlsxFile(bytes, "ClassWithAttributeKey");
        }

        [HttpGet("ClassWithHeaderList")]
        public async Task<IActionResult> ClassWithHeaderList()
        {
            var list = new List<ClassWithHeader>
            {
                new ClassWithHeader{Email = "a.b@mail.com",FirstName = "a", LastName = "b",Id = 1},
                new ClassWithHeader{Email = "c.d@mail.com",FirstName = "c", LastName = "d",Id = 2},
                new ClassWithHeader{Email = "e.f@mail.com",FirstName = "e", LastName = "f",Id = 2},
            };

            var headerNames = new List<string> { "id", "name", "second name", "email" };
            var bytes = ExportToExcel.ExportWorksheets(WorksheetDataModelCreator.CreateWorksheetDataModel(list, "ClassWithHeaderList", headerNames));
            return ContentHelper.ToXlsxFile(bytes, "ClassWithHeaderList");
        }

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var list = new List<ClassWithAttributeName>
            {
                new ClassWithAttributeName{Email = "a.b@mail.com",FirstName = "a", LastName = "b",Id = 1},
                new ClassWithAttributeName{Email = "c.d@mail.com",FirstName = "c", LastName = "d",Id = 2},
                new ClassWithAttributeName{Email = "e.f@mail.com",FirstName = "e", LastName = "f",Id = 2},
            };
            var list2 = new List<ClassWithHeader>
            {
                new ClassWithHeader{Email = "a.b@mail.com",FirstName = "a", LastName = "b",Id = 1},
                new ClassWithHeader{Email = "c.d@mail.com",FirstName = "c", LastName = "d",Id = 2},
                new ClassWithHeader{Email = "e.f@mail.com",FirstName = "e", LastName = "f",Id = 2},
            };

            var headerNames = new List<string> { "id", "name", "second name", "email" };

            var bytes = ExportToExcel.ExportWorksheets(new List<WorksheetDataModel>
            {
                WorksheetDataModelCreator.CreateWorksheetDataModel(list, "ClassWithAttributeName"),
                WorksheetDataModelCreator.CreateWorksheetDataModel(list2, "ClassWithHeaderList", headerNames)
            });
            return ContentHelper.ToXlsxFile(bytes, "ClassWithAttributeName");
        }
    }
}
