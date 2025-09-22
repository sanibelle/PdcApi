using Pdc.Domain.Exceptions;

namespace Pdc.WebAPI.Middlewares;

public class ExceptionHandlingMiddleware
{
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
            //TODO ajouter la gestion des erreurs de connexion à microsoft ailleurs qu'ici pour passer un 401
            await _next(context);
        }
        catch (Exception ex)
        {
            var status = GetStatusCode(ex);
            if (status >= 500)
                _logger.LogError(ex, "Unhandled exception.");
            else
                _logger.LogWarning(ex, "Handled exception with status {Status}.", status);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var env = context.RequestServices.GetRequiredService<IHostEnvironment>();
        var response = new
        {
            status = statusCode,
            message = exception.Message,
            detail = env.IsDevelopment() ? exception.StackTrace : null
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(response);
    }

    private static int GetStatusCode(Exception exception)
    {
        // TODO log not managed exception
        return exception switch
        {
            System.ComponentModel.DataAnnotations.ValidationException => StatusCodes.Status422UnprocessableEntity,
            FluentValidation.ValidationException => StatusCodes.Status422UnprocessableEntity,
            NotFoundException => StatusCodes.Status404NotFound,
            DuplicateException => StatusCodes.Status409Conflict,
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
