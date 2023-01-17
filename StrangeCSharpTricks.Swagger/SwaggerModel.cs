using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace StrangeCSharpTricks.Swagger
{
    public class SwaggerReader
    {
        public static OpenApiDocument ReadFromFile(string fileString)
        {
            var reader = new OpenApiStringReader();
            var model = reader.Read(fileString, out OpenApiDiagnostic diagnostic);

            var md = MarkdownWriter.CreatMarkDown(model);

            var path = @"C:\Users\michael\Documents\Visual Studio 2022\Projects\StrangeCSharpTricks\temp.md";

            File.WriteAllText(path, md);
            return model;
        }
    }

}