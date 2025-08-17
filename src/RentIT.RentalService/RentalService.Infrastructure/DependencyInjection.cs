using Microsoft.Extensions.DependencyInjection;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Infrastructure.Repositories;
namespace RentalService.Infrastructure
{
    /// <summary>
    /// Class to register services related to infrastructure layer
    /// </summary>
    public static  class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<IRentalRepository, RentalRepository>();
            return services;
        }
    }
}
