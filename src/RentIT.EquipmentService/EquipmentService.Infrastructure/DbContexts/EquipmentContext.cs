using EquipmentService.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquipmentService.Infrastructure.DbContexts
{
    public class EquipmentContext : DbContext
    {
        public virtual DbSet<Equipment> EquipmentItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public EquipmentContext(DbContextOptions options) : base(options){}

        public EquipmentContext(){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Equipment>().Property(prop => prop.Status)
                .HasConversion<string>();
        }
    }
}
