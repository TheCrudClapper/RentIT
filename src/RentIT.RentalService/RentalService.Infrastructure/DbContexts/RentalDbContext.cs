using Microsoft.EntityFrameworkCore;
using RentalService.Core.Domain.Entities;
using RentalService.Infrastructure.DbContexts.Interceptors;

namespace RentalService.Infrastructure.DbContexts
{
    public class RentalDbContext : DbContext
    {
        public virtual DbSet<Rental> Rentals { get; set; }

        public RentalDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public RentalDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Rental>()
                .HasQueryFilter(item => item.IsActive);
        }
    }
}
