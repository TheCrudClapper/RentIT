using System.Net.Http.Headers;

namespace EquipmentService.API.Handlers;

//Http Message handler that attaches Authentication header
//(if exist in current htpp context) to every outgoing request
public class BearerTokenHandler : DelegatingHandler
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    public BearerTokenHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        var token = httpContext?.Request.Headers.Authorization.FirstOrDefault();

        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return base.SendAsync(request, cancellationToken);
    }
}
