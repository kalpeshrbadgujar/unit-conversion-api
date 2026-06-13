using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UnitConversion.Api.Swagger;

/// <summary>
/// Documents enum query parameters as string names (e.g. "Length") instead of numeric values (e.g. 1).
/// </summary>
public sealed class EnumAsStringParameterFilter : IParameterFilter
{
    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {
        var type = Nullable.GetUnderlyingType(context.ApiParameterDescription.Type)
            ?? context.ApiParameterDescription.Type;

        if (!type.IsEnum)
        {
            return;
        }

        parameter.Schema = new OpenApiSchema
        {
            Type = "string",
            Enum = Enum.GetNames(type)
                .Select(name => new OpenApiString(name))
                .Cast<IOpenApiAny>()
                .ToList(),
        };

        parameter.Description = string.IsNullOrWhiteSpace(parameter.Description)
            ? $"Allowed values: {string.Join(", ", Enum.GetNames(type))}."
            : $"{parameter.Description} Allowed values: {string.Join(", ", Enum.GetNames(type))}.";
    }
}
