using RentalService.API.Handlers;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.Policies.Contracts;
using RentalService.Infrastructure.HttpClients;

namespace RentalService.API.Extensions;

public static class HttpClientsExtension
{
    public static IServiceCollection AddInfrastructureHttpClients(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddHttpClient<IUsersMicroserviceClient, UsersMicroserviceClient>(options =>
        {
            options.BaseAddress = new Uri($"http://{configuration["USERS_MICROSERVICE_NAME"]}:" +
                $"{configuration["USERS_MICROSERVICE_PORT"]}");
        })
            .AddHttpMessageHandler<BearerTokenHandler>()
            .AddPolicyHandler((serviceProvider, request) =>
            {
                var policies = serviceProvider.GetRequiredService<IUsersMicroservicePolicies>();
                return policies.GetCombinedPolicy();
            });

        services.AddHttpClient<IEquipmentMicroserviceClient, EquipmentMicroserviceClient>(options =>
        {
            options.BaseAddress = new Uri($"http://{configuration["EQUIPMENT_MICROSERVICE_NAME"]}:" +
                $"{configuration["EQUIPMENT_MICROSERVICE_PORT"]}");
        })
            .AddHttpMessageHandler<BearerTokenHandler>()
            .AddPolicyHandler((serviceProvider, request) =>
            {
                var policies = serviceProvider.GetRequiredService<IEquipmentMicroservicePolicies>();
                return policies.GetCombinedPolicy();
            });

        return services;
    }
}
