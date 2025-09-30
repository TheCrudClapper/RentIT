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
            //Add Db Context
            services.AddDbContext<EquipmentContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresDB")!
                    .Replace("$DB_NAME", Environment.GetEnvironmentVariable("DB_NAME") ?? "RentITEquipmentItems")
                    .Replace("$DB_PORT", Environment.GetEnvironmentVariable("DB_PORT") ?? "5432")
                    .Replace("$DB_USER", Environment.GetEnvironmentVariable("DB_USER") ?? "postgres")
                    .Replace("$DB_PASSWORD", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "admin")
                    .Replace("$DB_HOST", Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost"),
                    x => x.MigrationsAssembly("EquipmentService.Infrastructure"));
            });

            
            //Add Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            services.AddScoped<IUserEquipmentRepository, UserEquipmentRepository>();


            return services;
        }
    }
}
