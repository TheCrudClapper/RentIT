using RentIT.BlazorFrontend.Auth;
using RentIT.BlazorFrontend.ServiceContracts;
using RentIT.BlazorFrontend.Services.Localization;

namespace RentIT.BlazorFrontend.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddUILayer(this IServiceCollection services)
    {
        services.AddScoped<ILocalizationService, LocalizationService>();
        services.AddScoped<ISessionAuthenticationService, SessionAuthenticationService>();
        return services;
    }
}

