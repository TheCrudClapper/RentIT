using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReviewServices.Core.ServiceContracts;
using ReviewServices.Core.Caching;
using ReviewServices.Core.Services;
using ReviewService.Core.ServiceContracts;
using ReviewService.Core.Services;
using ReviewService.Core.RabbitMQ;
using ReviewService.Core.RabbitMQ.HostedServices;
using ReviewServices.Core.RabbitMQ.Publishers;
using ReviewService.Core.RabbitMQ.Publishers;

namespace ReviewServices.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreLayer(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = $"{Environment.GetEnvironmentVariable("REDIS_HOST") ?? "localhost"}:{Environment.GetEnvironmentVariable("REDIS_PORT")}" ?? "6379";
        });

        //Add Services
        services.AddScoped<ICachingHelper, CachingHelper>();
        services.AddScoped<IUserReviewService, UserReviewService>();
        services.AddScoped<IReviewService, ReviewsService>();
        services.AddScoped<IReviewAllowanceService, ReviewAllowanceService>();


        //Add RabbitMQ
        services.AddTransient<RabbitMQReviewAllowanceGrantedConsumer>();
        services.AddSingleton<IRabbitMQPublisher, RabbitMQPublisher>();
        services.AddHostedService<RabbitMQConsumersHostedService>();
        return services;    
    }
}
