using System.Net.Http.Headers;

namespace ReviewService.API.Handlers;

/// <summary>
/// Handler that automatically attaches Bearer Authorization header to outgoing requests
/// </summary>
public class BearerTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public BearerTokenHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        var authHeader = httpContext?.Request.Headers.Authorization.FirstOrDefault();

        if (!string.IsNullOrEmpty(authHeader))
        {
            string token = authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
                ? authHeader.Substring("Bearer ".Length).Trim()
                : authHeader.Trim();

            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }


        return await base.SendAsync(request, cancellationToken);
    }

}
public static class BearerTokenHandlerExtensions
{
    public static IServiceCollection AddAuthHeaderHandler(this IServiceCollection services)
    {
        services.AddTransient<BearerTokenHandler>();
        return services;
    }

}
