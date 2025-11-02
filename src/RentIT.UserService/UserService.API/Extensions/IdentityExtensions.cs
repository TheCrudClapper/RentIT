using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using UserService.Core.Domain.Entities;
using UserService.Infrastructure.DbContexts;

namespace UserService.API.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = true;
        })
        .AddEntityFrameworkStores<UsersDbContext>()
        .AddDefaultTokenProviders()
        .AddUserStore<UserStore<User, Role, UsersDbContext, Guid>>()
        .AddRoleStore<RoleStore<Role, UsersDbContext, Guid>>();
        return services;
    }
}
