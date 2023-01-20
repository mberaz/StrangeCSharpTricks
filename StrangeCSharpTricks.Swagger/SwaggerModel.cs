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
            return model;
        }
    }

}