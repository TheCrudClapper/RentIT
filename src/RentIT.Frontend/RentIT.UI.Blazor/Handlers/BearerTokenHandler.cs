using RentIT.UI.Core.InfrastructureContracts;
using RentIT.UI.Infrastructure.Stores;

namespace RentIT.BlazorFrontend.Handlers;

public class BearerTokenHandler : DelegatingHandler
{
    private readonly ITokenStore _tokenStore;
    private readonly ILogger<BearerTokenHandler> _logger;
    public BearerTokenHandler(ITokenStore tokenStore, ILogger<BearerTokenHandler> logger)
    {
        _tokenStore = tokenStore;
        _logger = logger;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("Authorization", $"Bearer {_tokenStore.GetAccessToken()}");
        _logger.LogInformation($"Authorization header was attached to request: {request.RequestUri}");
        return base.SendAsync(request, cancellationToken);
    }
}

public static class BearerTokenHandlerExtensions
{
    public static IServiceCollection AddBearerTokenHandler(this IServiceCollection services)
    {
        services.AddTransient<BearerTokenHandler>();
        return services;
    }
}