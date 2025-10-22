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
            request.Headers.Authorization = new AuthenticationHeaderValue(authHeader);

        return await base.SendAsync(request, cancellationToken);
    }
}
