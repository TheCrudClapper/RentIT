using Microsoft.EntityFrameworkCore;
using ReviewService.Core.Domain.Entities;
using ReviewService.Infrastructure.DbContexts.Interceptors;

namespace ReviewService.Infrastructure.DbContexts;

public class ReviewsDbContext : DbContext
{
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<ReviewAllowance> ReviewsAllowance { get; set;}
    public ReviewsDbContext() { }

    public ReviewsDbContext(DbContextOptions options) : base(options){ }

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
