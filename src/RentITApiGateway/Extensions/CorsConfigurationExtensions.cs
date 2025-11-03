using Microsoft.AspNetCore.Cors.Infrastructure;
using Polly;

namespace RentIT.ApiGateway.Extensions;

public static class CorsConfigurationExtensions
{
    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngular", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });
        return services;
    }
}
