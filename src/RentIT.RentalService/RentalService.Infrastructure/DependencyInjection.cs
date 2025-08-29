using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Infrastructure.DbContexts;
using RentalService.Infrastructure.Repositories;
namespace RentalService.Infrastructure
{
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
                    .Replace("$POSTGRES_DB", Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "RentITUsers")
                    .Replace("$POSTGRES_USER", Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres")
                    .Replace("$POSTGRES_PASSWORD", Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "admin")
                    .Replace("$HOST", Environment.GetEnvironmentVariable("HOST") ?? "localhost"),
                    x => x.MigrationsAssembly("RentalService.Infrastructure"));
            });
            services.AddScoped<IRentalRepository, RentalRepository>();
            return services;
        }
    }
}
