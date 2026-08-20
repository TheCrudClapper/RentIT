using RentIT.BlazorFrontend.Auth;

namespace RentIT.BlazorFrontend.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddUILayer(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}

