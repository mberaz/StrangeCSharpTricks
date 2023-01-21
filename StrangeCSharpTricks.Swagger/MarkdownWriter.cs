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
                lines.Add("#" + GetPathPrefix(path.Key));
                foreach (var operation in path.Value.Operations)
                {
                    lines.Add($"## {operation.Key.ToString().ToUpper()} {path.Key}");

                    //schema
                    lines.Add(" #### Input Parameters");

                    if (operation.Value.RequestBody != null)
                    {
                        var body = operation.Value.RequestBody.Content.FirstOrDefault(c => c.Key == "application/json").Value.Schema;
                        lines.Add($" Request Body Properties");
                        foreach (var property in body.Properties)
                        {
                            lines.Add($"       {property.Key} [{property.Value.Type}]");
                        }
                    }
                    else
                    {
                        lines.AddRange(operation.Value.Parameters.Select(param => "       " +
                                       param.Name +
                                       " [" + param.Schema.Type + (param.Schema.Nullable ? " Nullable" : string.Empty) + "]"
                                       + (param.Required ? " [Required] " : string.Empty)
                                       + $"From {param.In}"));
                    }

                    //responses

                    lines.Add(" #### Success Output responses");
                    var okResponse = operation.Value.Responses.FirstOrDefault(r => r.Key.StartsWith("20"));
                    lines.Add($" [{okResponse.Key}] {okResponse.Value.Description}");

                    var responseSchema = okResponse.Value.Content.Any() ?
                        okResponse.Value.Content.FirstOrDefault(c => c.Key == "application/json").Value?.Schema
                        : null;

                    if (responseSchema != null)
                    {
                        lines.Add($" Response Properties");

                        if (responseSchema.Items != null)
                        {
                            lines.Add(string.Empty);
                            lines.Add($" An array of:");
                            lines.Add(string.Empty);
                            foreach (var property in responseSchema.Items.Properties)
                            {
                                lines.Add($"       {property.Key} [{property.Value.Type}]");
                            }
                        }
                        else
                        {
                            foreach (var property in responseSchema.Properties)
                            {
                                lines.Add($"       {property.Key} [{property.Value.Type}]");
                            }
                        }

                    }

                    lines.Add(" #### Bad Output responses");
                    lines.AddRange(operation.Value.Responses.Where(r => !r.Key.StartsWith("20")).Select(response =>
                    $"       [{response.Key}] {response.Value.Description}"));

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
