using Microsoft.Extensions.DependencyInjection;
using UserService.Core.ServiceContracts;

namespace UserService.Core;
/// <summary>
/// Class to register services related to core layer
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddCoreLayer(this IServiceCollection services)
    {
        services.AddScoped<IUserService, Services.UserService>();
        return services;
    }
}

