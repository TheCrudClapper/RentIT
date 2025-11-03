using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Polly;
using UserService.Core.Domain.Entities;
using UserService.Infrastructure.DbContexts;
using UserService.Infrastructure.Seeders;

namespace UserService.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task MigrateDatabaseAsync(this WebApplication app, IServiceCollection services)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<UsersDbContext>();

            var policy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(2));

            await policy.ExecuteAsync(async () =>
            {
                await db.Database.MigrateAsync();
            });            
        }

        public static async Task SeedDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            var context = scope.ServiceProvider.GetRequiredService<UsersDbContext>();

            await AppDbSeeder.Seed(context, userManager, roleManager);
        }
    }
}
