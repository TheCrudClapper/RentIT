using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Core.Domain.RepositoryContracts;
using UserService.Infrastructure.DbContexts;
using UserService.Infrastructure.Repositories;
namespace UserService.Infrastructure
{
    /// <summary>
    /// Class to register services related to infrastructure layer
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UsersDbContext>(options =>
            {
                //In case we run program from IDE, provided values if env variables are null
                options.UseNpgsql(configuration.GetConnectionString("PostgresDB")!
                    .Replace("$DB_NAME", Environment.GetEnvironmentVariable("DB_NAME") ?? "RentITUsers")
                    .Replace("$DB_PORT", Environment.GetEnvironmentVariable("DB_PORT") ?? "5432")
                    .Replace("$DB_USER", Environment.GetEnvironmentVariable("DB_USER") ?? "postgres")
                    .Replace("$DB_PASSWORD", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "admin")
                    .Replace("$DB_HOST",Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost"),
                    x => x.MigrationsAssembly("UserService.Infrastructure"));
            });
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
