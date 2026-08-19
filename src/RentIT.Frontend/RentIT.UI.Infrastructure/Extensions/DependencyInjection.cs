using Microsoft.Extensions.DependencyInjection;
using RentIT.UI.Core.HttpClientContracts;
using RentIT.UI.Infrastructure.HttpClients;

namespace RentIT.UI.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddHttpClient<IAuthHttpClient, AuthHttpClient>();
        return services;
    }
}

