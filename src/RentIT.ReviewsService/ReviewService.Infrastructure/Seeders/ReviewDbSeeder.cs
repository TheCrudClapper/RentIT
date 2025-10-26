using Microsoft.EntityFrameworkCore;
using ReviewServices.Core.Domain.Entities;
using ReviewServices.Infrastructure.DbContexts;
namespace ReviewService.Infrastructure.Seeders;

public static class ReviewDbSeeder
{
    public static async Task Seed(ReviewsDbContext context)
    {
        if (!await context.Reviews.AnyAsync() && !await context.ReviewsAllowance.AnyAsync())
        {
            await context.Reviews.AddAsync(new Review
            {
                Id = Guid.Parse("573DA406-3CF4-47F8-A216-580D780941E5"),
                DateCreated = DateTime.UtcNow,
                IsActive = true,
                UserId = Guid.Parse("D8862A46-6E4B-438D-AFB2-BD9498B2E708"),
                EquipmentId = Guid.Parse("54A58B0B-3BCD-46A5-80AF-101074FE8CDD"),
                RentalId = Guid.Parse("16677793-B8DD-4698-9C5E-AAB6211CFD07"),
                Description = "Such a nice thing, ill recommend",
                Rating = 4.2m
            });

            await context.ReviewsAllowance.AddAsync(new ReviewAllowance
            {
                Id = Guid.Parse("B5A65471-8832-4318-B2B6-CD44BAA3F928"),
                DateCreated = DateTime.UtcNow,
                IsActive = false,
                UserId = Guid.Parse("D8862A46-6E4B-438D-AFB2-BD9498B2E708"),
                EquipmentId = Guid.Parse("54A58B0B-3BCD-46A5-80AF-101074FE8CDD"),
                RentalId = Guid.Parse("16677793-B8DD-4698-9C5E-AAB6211CFD07"),
                DateDeleted = DateTime.UtcNow + TimeSpan.FromSeconds(1),
            });
            await context.SaveChangesAsync();

        }
    }
}