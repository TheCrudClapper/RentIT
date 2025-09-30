using EquipmentService.Core.Caching;
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
    /// Class to register services related to core layer
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

            //Add Redis Cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{Environment.GetEnvironmentVariable("REDIS_HOST") ?? "localhost"}:{Environment.GetEnvironmentVariable("REDIS_PORT")}" ?? "6379";
            });

            //Add CachingHelper
            services.AddScoped<ICachingHelper, CachingHelper>();

            return services;
        }
    }
}
