using ReviewService.API.Handlers;
using ReviewService.Core.Domain.HttpClientContracts;
using ReviewService.Infrastructure.HttpClients;

namespace ReviewService.API.Extensions;

public static class HttpClientsExtension
{
    public static IServiceCollection AddInfrastructureHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IUsersMicroserviceClient, UsersMicroserviceClient>(options =>
        {
            options.BaseAddress = new Uri($"http://{configuration["USERS_MICROSERVICE_NAME"]}" +
                $":{configuration["USERS_MICROSERVICE_PORT"]}");
        })
            .AddHttpMessageHandler<BearerTokenHandler>();

                services.AddHttpClient<IRentalMicroserviceClient, RentalMicroserviceClient>(options =>
                {
                    options.BaseAddress = new Uri($"http://{configuration["RENTAL_MICROSERVICE_NAME"]}" +
                        $":{configuration["RENTAL_MICROSERVICE_PORT"]}");
                })
                    .AddHttpMessageHandler<BearerTokenHandler>();
        return services;
    }
}
