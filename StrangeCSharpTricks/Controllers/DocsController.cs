using Microsoft.AspNetCore.Mvc;
using StrangeCSharpTricks.Swagger;
using System.Net.Http;
using System.Threading.Tasks;

namespace StrangeCSharpTricks.DictionaryIsTheNewIf.Controllers
{
    [Route("Docs")]
    public class DocsController : Controller
    {
        [HttpGet("")]
        public async Task<IActionResult> Index([FromQuery] string path)
        {
            var fileString = await DownloadFile(path);

            var model = SwaggerReader.ReadFromFile(fileString);

            var md = MarkdownWriter.CreateMarkDown(model);

            //TODO use real path
            var filePath = @"...temp.md";

            System.IO.File.WriteAllText(filePath, md);
            return View();
        }


        private async Task<string> DownloadFile(string url)
        {
            var result = await new HttpClient().GetAsync(url);

            return await result.Content.ReadAsStringAsync();
        }

    }
}
