using Microsoft.Extensions.DependencyInjection;
using RentalService.Core.ServiceContracts;
using RentalService.Core.Services;
namespace RentalService.Core;

/// <summary>
/// Class to register services related to infrastructure layer
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddCoreLayer(this IServiceCollection services)
    {
        services.AddScoped<IRentalService, Services.RentalService>();
        return services;
    }
}
