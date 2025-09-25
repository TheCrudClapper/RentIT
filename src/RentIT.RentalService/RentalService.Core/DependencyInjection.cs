using Microsoft.Extensions.DependencyInjection;
using RentalService.Core.RabbitMQ;
using RentalService.Core.ServiceContracts;
using RentalService.Core.Services;
using RentalService.Core.Validators.Contracts;
using RentalService.Core.Validators.Implementations;
namespace RentalService.Core;

/// <summary>
/// Class to register services related to infrastructure layer
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddCoreLayer(this IServiceCollection services)
    {
        //Add Services
        services.AddScoped<IUserRentalService, UserRentalService>();
        services.AddScoped<IRentalService, Services.RentalService>();

        //Add Validators
        services.AddScoped<IRentalValidator, RentalValidator>();
        services.AddScoped<IUserRentalValidator, UserRentalValidator>();

        //Add Redis Cache
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = $"{Environment.GetEnvironmentVariable("REDIS_HOST") ?? "localhost"}:{Environment.GetEnvironmentVariable("REDIS_PORT")}" ?? "6379";
        });

        //Add Consumers
        services.AddTransient<IRabbitMQEquipmentDeletedConsumer, RabbitMQEquipmentDeletedConsumer>();
        services.AddHostedService<RabbitMQEquipmentDeleteHostedService>();

        return services;
    }
}
