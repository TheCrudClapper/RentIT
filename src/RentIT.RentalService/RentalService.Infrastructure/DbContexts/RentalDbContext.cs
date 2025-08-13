using Microsoft.EntityFrameworkCore;
using RentalService.Core.Domain.Entities;

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
    }
}
