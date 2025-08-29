using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;
using EquipmentService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace EquipmentService.Infrastructure
{
    /// <summary>
    /// Class to register services related to infrastructure layer
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EquipmentContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresDB")!
                    .Replace("$POSTGRES_DB", Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "RentITEquipmentItems")
                    .Replace("$POSTGRES_USER", Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres")
                    .Replace("$POSTGRES_PASSWORD", Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "admin")
                    .Replace("$HOST", Environment.GetEnvironmentVariable("HOST") ?? "localhost"),
                    x => x.MigrationsAssembly("EquipmentService.Infrastructure"));
            });
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            return services;
        }
    }
}
