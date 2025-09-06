using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Core.Domain.Entities;
using UserService.Core.Enums;
using UserService.Core.Mappings;
using UserService.Infrastructure.DbContexts;

namespace UserService.Infrastructure.Seeders
{
    public static class AppDbSeeder
    {
        public static async Task Seed(UsersDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            //Check wheter roles exist if not add them
            if (!await roleManager.RoleExistsAsync(UserRoleOption.Admin.ToString()))
                await roleManager.CreateAsync(UserRoleOption.Admin.ToRoleEntity());

            if(!await roleManager.RoleExistsAsync(UserRoleOption.User.ToString()))
                await roleManager.CreateAsync(UserRoleOption.User.ToRoleEntity());

            //Add sample users
            if (!await context.Users.AnyAsync())
            {
                User user1 = new User
                {
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Email = "jankowalski123@gmail.com",
                    DateCreated = DateTime.UtcNow,
                    IsActive = true,
                    Id = Guid.NewGuid(),
                    UserName = "jankowalski123@gmail.com"
                };

                User user2 = new User
                {
                    FirstName = "Jakub",
                    LastName = "Tester",
                    Email = "jakubtester12@gmail.com",
                    DateCreated = DateTime.UtcNow,
                    IsActive = true,
                    Id = Guid.NewGuid(),
                    UserName = "jakubtester12@gmail.com"
                };

                await userManager.CreateAsync(user1,
                    "Test123#");

                await userManager.CreateAsync(user2,
                   "Test123#");

                await userManager.AddToRoleAsync(user1, UserRoleOption.Admin.ToString());
                await userManager.AddToRoleAsync(user2, UserRoleOption.User.ToString());
            }
        }


    }
}
