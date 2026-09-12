using Microsoft.Extensions.DependencyInjection;
using RentIT.UI.Core.ServiceContracts;
using RentIT.UI.Core.Services;

namespace RentIT.UI.Core.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreLayer(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}

