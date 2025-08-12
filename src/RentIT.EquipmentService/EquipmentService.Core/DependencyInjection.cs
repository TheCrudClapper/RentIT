using Microsoft.Extensions.DependencyInjection;
namespace EquipmentService.Core
{
    /// <summary>
    /// Class to register services related to infrastructure layer
    /// </summary>
    public static  class DependencyInjection
    {
        public static IServiceCollection AddCoreLayer(this IServiceCollection services)
        {
            return services;
        }
    }
}
