using EquipmentService.Core.ServiceContracts.CategoryContracts;
using EquipmentService.Core.ServiceContracts.Equipment;
using EquipmentService.Core.Services.CategoryServices;
using EquipmentService.Core.Services.EquipmentServices;
using EquipmentService.Core.Validators;
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
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserEquipmentService, UserEquipmentService>();
            services.AddScoped<IEquipmentService, Services.EquipmentServices.EquipmentService>();
            services.AddScoped<EquipmentValidator, EquipmentValidator>();
            services.AddScoped<UserEquipmentValidator, UserEquipmentValidator>();
            return services;
        }
    }
}
