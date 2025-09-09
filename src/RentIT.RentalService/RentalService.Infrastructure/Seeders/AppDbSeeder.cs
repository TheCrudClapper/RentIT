using Microsoft.EntityFrameworkCore;
using RentalService.Core.Domain.Entities;
using RentalService.Infrastructure.DbContexts;

namespace RentalService.Infrastructure.Seeders
{
    public static class AppDbSeeder
    {
        public static async Task Seed(RentalDbContext context)
        {
            if (!await context.Rentals.AnyAsync())
            {
                await context.AddRangeAsync(
                   new Rental
                   {
                       Id = Guid.NewGuid(),
                       IsActive = true,
                       DateCreated = DateTime.UtcNow,
                       StartDate = new DateTime(2025, 9, 12, 0, 0, 0, DateTimeKind.Utc),
                       EndDate = new DateTime(2025, 9, 15, 0, 0, 0, DateTimeKind.Utc),
                       RentalPrice = 80,
                       EquipmentId = Guid.Parse("E7425BFC-840E-4C76-9348-A52EA6BAC18F"),
                       UserId = Guid.Parse("D6D7EDCA-E2E0-4F08-A5DD-B4749BD8830A"),
                       ReturnedDate = null
                   },
                   new Rental
                   {
                       Id = Guid.NewGuid(),
                       IsActive = true,
                       DateCreated = DateTime.UtcNow,
                       StartDate = new DateTime(2025, 9, 13, 0, 0, 0, DateTimeKind.Utc),
                       EndDate = new DateTime(2025, 9, 16, 0, 0, 0, DateTimeKind.Utc),
                       RentalPrice = 120,
                       EquipmentId = Guid.Parse("E0CBA656-580D-4C26-8209-0C8093D30DE6"),
                       UserId = Guid.Parse("D8862A46-6E4B-438D-AFB2-BD9498B2E708"),
                       ReturnedDate = null
                   }
                   );
            }

            await context.SaveChangesAsync();
        }
    }
}
