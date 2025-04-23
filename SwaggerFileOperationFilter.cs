using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SwaggerFileOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileParams = context.MethodInfo.GetParameters()
            .Where(p => p.ParameterType == typeof(IFormFile));

        if (fileParams.Any())
        {
            operation.RequestBody.Content["multipart/form-data"] = new OpenApiMediaType
            {
                Schema = new OpenApiSchema
                {
                    Type = "object",
                    Properties =
                    {
                        ["file"] = new OpenApiSchema
                        {
                            Type = "string",
                            Format = "binary",
                            Description = "Fichier image à uploader"
                        },
                        ["annonceId"] = new OpenApiSchema
                        {
                            Type = "integer",
                            Format = "int32",
                            Nullable = true,
                            Description = "ID de l'annonce associée (optionnel)"
                        }
                    },
                    Required = new HashSet<string> { "file" }
                }
            };
        }
    }
}