using Microsoft.Extensions.DependencyInjection;
using RentIT.UI.Core.HttpClientContracts;
using RentIT.UI.Core.InfrastructureContracts;
using RentIT.UI.Infrastructure.Handlers;
using RentIT.UI.Infrastructure.HttpClients;
using RentIT.UI.Infrastructure.Stores;

namespace RentIT.UI.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddBearerTokenHandler();
        services.AddHttpClient<IAuthHttpClient, AuthHttpClient>();
        services.AddHttpClient<IUserEquipmentHttpClient, UserEquipmentHttpClient>()
           .AddHttpMessageHandler<BearerTokenHandler>();
        services.AddHttpClient<ICategoriesHttpClient, CategoriesHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        //Stores
        services.AddSingleton<ITokenStore, TokenStore>();
        return services;
    }
}

