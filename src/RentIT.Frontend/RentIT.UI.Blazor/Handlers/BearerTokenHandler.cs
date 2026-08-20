using RentIT.UI.Core.InfrastructureContracts;
using RentIT.UI.Infrastructure.Stores;

namespace RentIT.BlazorFrontend.Handlers;

public class BearerTokenHandler : DelegatingHandler
{
    private readonly ITokenStore _tokenStore;
    public BearerTokenHandler(ITokenStore tokenStore)
    {
        _tokenStore = tokenStore;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("Authorization", $"Bearer {_tokenStore.GetAccessToken()}");
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