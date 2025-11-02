using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReviewService.Core.Domain.HttpClientContracts;
using ReviewService.Core.Domain.RepositoryContracts;
using ReviewService.Infrastructure.HttpClients;
using ReviewService.Infrastructure.Repositories;
using ReviewServices.Infrastructure.DbContexts;
namespace ReviewServices.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ReviewsDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresDB")!
               .Replace("$DB_NAME", Environment.GetEnvironmentVariable("DB_NAME") ?? "RentITReviews")
               .Replace("$DB_PORT", Environment.GetEnvironmentVariable("DB_PORT") ?? "5432")
               .Replace("$DB_USER", Environment.GetEnvironmentVariable("DB_USER") ?? "postgres")
               .Replace("$DB_PASSWORD", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "admin")
               .Replace("$DB_HOST", Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost"),
               x => x.MigrationsAssembly("ReviewService.Infrastructure"));

        });

        services.AddScoped<IUserReviewRepository, UserReviewRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IReviewAllowanceRepository, ReviewAllowanceRepository>();

        return services;
    }

}

