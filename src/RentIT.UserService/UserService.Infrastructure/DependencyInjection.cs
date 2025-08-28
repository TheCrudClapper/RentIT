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
                options.UseNpgsql(configuration.GetConnectionString("PostgresDB")!
                    .Replace("$POSTGRES_DB", Environment.GetEnvironmentVariable("POSTGRES_DB"))
                    .Replace("$POSTGRES_USER", Environment.GetEnvironmentVariable("POSTGRES_USER"))
                    .Replace("$POSTGRES_PASSWORD", Environment.GetEnvironmentVariable("POSTGRES_PASSWORD"))
                    .Replace("$HOST",Environment.GetEnvironmentVariable("HOST")),
                    x => x.MigrationsAssembly("UserService.Infrastructure"));
            });
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
