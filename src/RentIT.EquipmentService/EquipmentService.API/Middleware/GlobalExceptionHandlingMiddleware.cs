using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace EquipmentService.API.Middleware;

/// <summary>
/// Middleware that provides centralized exception handling for HTTP requests in an ASP.NET Core application.
/// </summary>
/// <remarks>This middleware intercepts unhandled exceptions that occur during the processing of HTTP
/// requests and logs the exception details using the configured <see cref="ILogger{TCategoryName}"/>. It also sets
/// the HTTP response status code to 500 (Internal Server Error) and can optionally return a custom error response
/// to the client.  To use this middleware, add it to the request pipeline in the `Startup` class or `Program.cs`
/// using the <c>app.UseMiddleware&lt;GlobalExceptionHandlingMiddleware&gt;()</c> method.</remarks>
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next,
        ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {

            if (ex is TaskCanceledException or OperationCanceledException)
            {
                _logger.LogInformation("Request canceled by user");
                httpContext.Response.StatusCode = 499;
                await httpContext.Response.WriteAsync(ex.Message);
                return;
            }

            if (ex.InnerException != null)
            {
                _logger.LogError($"{ex.InnerException.GetType().ToString()}:" +
                    $" {ex.InnerException.Message}");
            }

            _logger.LogError($"{ex.GetType().ToString()}: {ex.Message}");

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails details = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Title = "Servcer Error",
                Detail = "An internal server error has occured",
            };

            string json = JsonSerializer.Serialize(details);

            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsync(json);
        }

    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class GlobalExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}
