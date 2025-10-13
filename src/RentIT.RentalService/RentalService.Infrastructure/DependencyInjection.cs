using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Infrastructure.DbContexts;
using RentalService.Infrastructure.Repositories;
namespace RentalService.Infrastructure;


/// <summary>
/// Class to register services related to infrastructure layer
/// </summary>
public static  class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RentalDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresDB")!
                .Replace("$DB_NAME", Environment.GetEnvironmentVariable("DB_NAME") ?? "RentITRentals")
                .Replace("$DB_PORT", Environment.GetEnvironmentVariable("DB_PORT") ?? "5432")
                .Replace("$DB_USER", Environment.GetEnvironmentVariable("DB_USER") ?? "postgres")
                .Replace("$DB_PASSWORD", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "admin")
                .Replace("$DB_HOST", Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost"),
                x => x.MigrationsAssembly("RentalService.Infrastructure"));
        });
        services.AddScoped<IRentalRepository, RentalRepository>();
        services.AddScoped<IUserRentalRepository, UserRentalRepository>();

     
        return services;
    }
}
