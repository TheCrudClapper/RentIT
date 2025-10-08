using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ReviewService.API.Middleware;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
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
            if(ex is TaskCanceledException or OperationCanceledException)
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

            ProblemDetails problemDetails = new ProblemDetails()
            {
                Title = "Something went wrong",
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Detail = "An internal server error has occured",
            };
            
            var json = JsonSerializer.Serialize(problemDetails);
            
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsync(json);
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}
