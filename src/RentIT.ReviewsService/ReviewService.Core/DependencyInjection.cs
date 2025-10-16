using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReviewServices.Core.Caching;
using ReviewServices.Core.ServiceContracts;
using ReviewServices.Core.Services;

namespace ReviewServices.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreLayer(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = $"{Environment.GetEnvironmentVariable("REDIS_HOST") ?? "localhost"}:{Environment.GetEnvironmentVariable("REDIS_PORT")}" ?? "6379";
        });

        //Add Caching Helper
        services.AddScoped<ICachingHelper, CachingHelper>();
        services.AddScoped<IUserReviewService, UserReviewService>();
        return services;    
    }
}
