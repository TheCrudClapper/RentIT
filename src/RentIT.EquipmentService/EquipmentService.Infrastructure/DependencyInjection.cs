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
            return services;
        }
    }
}
