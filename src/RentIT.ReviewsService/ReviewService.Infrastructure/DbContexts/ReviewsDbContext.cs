using Microsoft.EntityFrameworkCore;
using ReviewService.Core.Domain.Entities.Review;
using ReviewService.Core.Domain.Entities.ReviewAllowance;
using ReviewServices.Infrastructure.DbContexts.Interceptors;

namespace ReviewServices.Infrastructure.DbContexts;

public class ReviewsDbContext : DbContext
{
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<ReviewAllowance> ReviewsAllowance { get; set; }
    public ReviewsDbContext() { }

    public ReviewsDbContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>()
            .HasQueryFilter(item => item.IsActive);
        modelBuilder.Entity<ReviewAllowance>()
            .HasQueryFilter(item => item.IsActive);
    }
}
