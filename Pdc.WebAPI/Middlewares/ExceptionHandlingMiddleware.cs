using Pdc.Domain.Exceptions;

namespace Pdc.WebAPI.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var env = context.RequestServices.GetRequiredService<IHostEnvironment>();
        var response = new
        {
            status = statusCode,
            message = exception.Message,
            detail = env.IsDevelopment() ? exception.StackTrace : null
        };
        if (statusCode >= 500)
            _logger.LogError(exception, "Unhandled exception.");
        else
            _logger.LogWarning(exception, "Handled exception with status {Status}.", statusCode);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(response);
    }

    private int GetStatusCode(Exception exception)
    {
        // TODO log not managed exception
        return exception switch
        {
            System.ComponentModel.DataAnnotations.ValidationException => StatusCodes.Status422UnprocessableEntity,
            FluentValidation.ValidationException => StatusCodes.Status422UnprocessableEntity,
            NotFoundException => StatusCodes.Status404NotFound,
            DuplicateException => StatusCodes.Status409Conflict,
            MissingChangeRecordException => StatusCodes.Status400BadRequest,
            AuthException => StatusCodes.Status401Unauthorized,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            ForbiddenException => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}

// Extension method pour faciliter l'enregistrement du middleware
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
