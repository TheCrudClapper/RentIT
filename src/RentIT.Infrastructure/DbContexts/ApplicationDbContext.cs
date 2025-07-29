using Microsoft.EntityFrameworkCore;
using RentIT.Core.Domain.Entities;

namespace RentIT.Infrastructure.DbContexts
{
    public class ApplicationDbContext :DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Equipment> EquipmentItems { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }
        
        public ApplicationDbContext(DbContextOptions options) :base(options)
        {
        }
        public ApplicationDbContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Equipment>().Property(prop => prop.Status)
                .HasConversion<string>();
        }

    }
}
