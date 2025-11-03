using EquipmentService.Infrastructure.DbContexts;
using EquipmentService.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace EquipmentService.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task MigrateDatabaseAsync(this WebApplication app, IServiceCollection services)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<EquipmentContext>();

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
            var context = scope.ServiceProvider.GetRequiredService<EquipmentContext>();
            await AppDbSeeder.Seed(context);
        }
    }

}
