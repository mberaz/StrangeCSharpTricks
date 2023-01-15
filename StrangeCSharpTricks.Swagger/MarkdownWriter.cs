using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangeCSharpTricks.Swagger
{
    public class MarkdownWriter
    {//http://localhost:31697/swagger/v1/swagger.json
        public static string CreatMarkDown(OpenApiDocument openApi)
        {
            var lines = new List<string>();

            lines.Add($"## {openApi.Info.Title} {openApi.Info.Version}");
            lines.Add(string.Empty);

            foreach (var path in openApi.Paths)
            {
                lines.Add(GetPathPrefix(path.Key));
                foreach (var operation in path.Value.Operations)
                {
                    lines.Add($"## {operation.Key.ToString().ToUpper()} {path.Key}");

                    lines.Add($"Tags {string.Join(", ", operation.Value.Tags)}");
                    //schema
                    //responses
                }
            }

            return string.Join(Environment.NewLine, lines);
        }


        private static string GetPathPrefix(string path)
        {
            var pathParts = path.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            return pathParts.First();
        }
    }
}
