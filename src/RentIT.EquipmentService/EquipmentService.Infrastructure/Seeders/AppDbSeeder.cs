using EquipmentService.Core.Domain.Entities;
using EquipmentService.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EquipmentService.Infrastructure.Seeders
{
    public static class AppDbSeeder
    {
        public static async Task Seed(EquipmentContext context)
        {
            //Add sample categories
            if (!await context.Categories.AnyAsync())
            {
                await context.Categories.AddRangeAsync(
                new Category
                {
                    DateCreated = DateTime.UtcNow,
                    Id = Guid.Parse("71A890DE-C3DF-41AC-A22E-F0332326EBC5"),
                    IsActive = true,
                    Name = "Gaming Console"
                },

                new Category
                {
                    DateCreated = DateTime.UtcNow,
                    Id = Guid.Parse("469683FB-9A89-4C1D-9B3E-D35A24157ED8"),
                    IsActive = true,
                    Name = "PC"
                });
            }

            if (!await context.EquipmentItems.AnyAsync())
            {
                await context.EquipmentItems.AddRangeAsync(
                new Equipment
                {
                    Id = Guid.Parse("E7425BFC-840E-4C76-9348-A52EA6BAC18F"),
                    CategoryId = Guid.Parse("71A890DE-C3DF-41AC-A22E-F0332326EBC5"),
                    CreatedByUserId = Guid.Parse("D8862A46-6E4B-438D-AFB2-BD9498B2E708"),
                    DateCreated = DateTime.UtcNow,
                    IsActive = true,
                    Name = "PlayStation 5 PRO",
                    SerialNumber = "S01-6433",
                    RentalPricePerDay = 20,
                    Status = RentStatusEnum.Rented,
                    Notes = "Console fully working, games included"
                },
                 new Equipment
                 {
                     Id = Guid.Parse("E0CBA656-580D-4C26-8209-0C8093D30DE6"),
                     CategoryId = Guid.Parse("469683FB-9A89-4C1D-9B3E-D35A24157ED8"),
                     CreatedByUserId = Guid.Parse("D6D7EDCA-E2E0-4F08-A5DD-B4749BD8830A"),
                     DateCreated = DateTime.UtcNow,
                     IsActive = true,
                     Name = "PC R5600 + RX 7080 XT + 32GB RAM",
                     SerialNumber = "PC-6433",
                     RentalPricePerDay = 40,
                     Status = RentStatusEnum.Rented,
                     Notes = "Im renting fully working pc, without drives"
                 }
                );
            }

            await context.SaveChangesAsync();
        }


    }
}
