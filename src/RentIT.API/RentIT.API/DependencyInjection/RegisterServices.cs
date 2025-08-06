using RentIT.Core.CustomValidators;
using RentIT.Core.Domain.RepositoryContracts;
using RentIT.Core.ServiceContracts;
using RentIT.Core.Services;
using RentIT.Infrastructure.Repositories;

namespace RentIT.API.DependencyInjection
{
    public static class RegisterServices
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            //Add Repositories 
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IEquipmentRepository , EquipmentRepository>();

            //Add Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<IEquipmentService, EquipmentService>();

            //Add Utility Services/Classes
        }
    }
}

