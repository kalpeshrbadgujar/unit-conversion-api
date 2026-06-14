using System.Net;
using System.Text.Json;
using FluentValidation;
using UnitConversion.Domain.Exceptions;

namespace UnitConversion.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(
                "Validation failed for {Method} {Path} with {ValidationErrorCount} errors",
                context.Request.Method,
                context.Request.Path,
                ex.Errors.Count());

            await WriteValidationErrorAsync(context, ex);
        }
        catch (UnitNotFoundException ex)
        {
            _logger.LogWarning(
                "Unit not found for {Method} {Path}: {UnitCode}",
                context.Request.Method,
                context.Request.Path,
                ex.UnitCode);

            await WriteErrorAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (IncompatibleUnitCategoryException ex)
        {
            _logger.LogWarning(
                "Incompatible units for {Method} {Path}: {FromUnit} -> {ToUnit}",
                context.Request.Method,
                context.Request.Path,
                ex.FromUnit,
                ex.ToUnit);

            await WriteErrorAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception processing {Method} {Path}", context.Request.Method, context.Request.Path);
            await WriteErrorAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.");
        }
    }

    private static async Task WriteValidationErrorAsync(HttpContext context, ValidationException exception)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/json";

        var payload = JsonSerializer.Serialize(new
        {
            message = "Validation failed.",
            errors = exception.Errors.Select(error => new
            {
                propertyName = error.PropertyName,
                errorMessage = error.ErrorMessage,
            }),
        }, JsonOptions);

        await context.Response.WriteAsync(payload);
    }

    private static async Task WriteErrorAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var payload = JsonSerializer.Serialize(new { message }, JsonOptions);
        await context.Response.WriteAsync(payload);
    }
}
