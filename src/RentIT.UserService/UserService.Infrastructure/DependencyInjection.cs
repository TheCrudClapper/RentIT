using Microsoft.Extensions.DependencyInjection;
using UserService.Core.Domain.RepositoryContracts;
using UserService.Infrastructure.Repositories;
namespace UserService.Infrastructure
{
    /// <summary>
    /// Class to register services related to infrastructure layer
    /// </summary>
    public static  class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
