using Microsoft.Extensions.DependencyInjection;
using UserService.Core.ServiceContracts;
using UserService.Core.Services;

namespace UserService.Core;
/// <summary>
/// Class to register services related to core layer
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddCoreLayer(this IServiceCollection services)
    {
        services.AddScoped<IUserService, Services.UserService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}

