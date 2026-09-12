using EquipmentService.API.Handlers;
using EquipmentService.Core.Domain.HtppClientContracts;
using EquipmentService.Core.Policies.Contracts;
using EquipmentService.Infrastructure.HttpClients;

namespace EquipmentService.API.Extensions;

public static class HttpClientsExtension
{
    public static IServiceCollection AddInfrastructureHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IUsersMicroserviceClient, UsersMicroserviceClient>(client =>
        {
            client.BaseAddress = new Uri($"http://{configuration["USERS_MICROSERVICE_NAME"]}:{configuration["USERS_MICROSERVICE_PORT"]}");
        })
            .AddHttpMessageHandler<BearerTokenHandler>()
            .AddPolicyHandler((serviceProvider, request) =>
            {
                var policies = serviceProvider.GetRequiredService<IUsersMicroservicePolicies>();
                return policies.GetCombinedPolicy();
            })
            ;
        return services;
    }
}
