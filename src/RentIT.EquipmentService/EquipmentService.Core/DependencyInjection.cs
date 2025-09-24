using EquipmentService.Core.Policies.Contracts;
using EquipmentService.Core.Policies.Implementations;
using EquipmentService.Core.RabbitMQ;
using EquipmentService.Core.ServiceContracts.CategoryContracts;
using EquipmentService.Core.ServiceContracts.Equipment;
using EquipmentService.Core.Services.CategoryServices;
using EquipmentService.Core.Services.EquipmentServices;
using EquipmentService.Core.Validators.Implementations;
using EquipmentService.Core.Validators.ValidatorContracts;
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
            //Add Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserEquipmentService, UserEquipmentService>();
            services.AddScoped<IEquipmentService, Services.EquipmentServices.EquipmentService>();

            //Add Validators
            services.AddScoped<IEquipmentValidator, EquipmentValidator>();
            services.AddScoped<IUserEquipmentValidator, UserEquipmentValidator>();

            //Add RabbitMQ Components
            services.AddTransient<IRabbitMQPublisher, RabbitMQPublisher>();

            return services;
        }
    }
}
