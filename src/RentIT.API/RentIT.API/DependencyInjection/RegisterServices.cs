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

            //Add Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            

            //Add Utility Services/Classes
        }
    }
}
