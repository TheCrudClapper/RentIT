using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace EquipmentService.Infrastructure
{
    /// <summary>
    /// Class to register services related to infrastructure layer
    /// </summary>
    public static  class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}
