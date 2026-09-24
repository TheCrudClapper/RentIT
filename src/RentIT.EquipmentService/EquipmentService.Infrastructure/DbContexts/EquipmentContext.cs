using EquipmentService.Core.Domain.Entities.Categories;
using EquipmentService.Core.Domain.Entities.Equipments;
using EquipmentService.Core.Domain.Entities.Listing;
using EquipmentService.Core.Domain.Entities.Shared;
using EquipmentService.Infrastructure.DbContexts.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace EquipmentService.Infrastructure.DbContexts
{
    public class EquipmentContext : DbContext
    {
        public virtual DbSet<Equipment> EquipmentItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<RentalListing> RentalListings { get; set; }
        public virtual DbSet<ListingImage> ListingImages { get; set; }
        public virtual DbSet<EquipmentImage> EquipmentImage { get; set; }

        public EquipmentContext(DbContextOptions options) : base(options) { }

        public EquipmentContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Equipment
            modelBuilder.Entity<Equipment>().HasQueryFilter(item => item.IsActive);
            modelBuilder.Entity<Equipment>().HasMany<RentalListing>(x => x.Listings).WithOne(x => x.Equipment);
            modelBuilder.Entity<Equipment>().HasMany<EquipmentImage>(x => x.Images).WithOne(x => x.Equipment);
            modelBuilder.Entity<Equipment>().ToTable("RentIt.Equipments");

            //Listings
            modelBuilder.Entity<RentalListing>().HasQueryFilter(item => item.IsActive);
            modelBuilder.Entity<RentalListing>().OwnsOne<Currency>(x => x.PricePerDay);
            modelBuilder.Entity<RentalListing>().OwnsOne<Currency>(x => x.PenaltyFeePerDay);
            modelBuilder.Entity<RentalListing>().HasMany<ListingImage>(x => x.Images).WithOne(x => x.RentalListing);
            modelBuilder.Entity<RentalListing>().Property(x => x.ListingStatus).HasConversion<string>();
            modelBuilder.Entity<RentalListing>().HasMany(x => x.Images).WithOne(x => x.RentalListing);
            modelBuilder.Entity<RentalListing>().ToTable("RentIt.RentalListings");

            //Categories
            modelBuilder.Entity<Category>().HasQueryFilter(item => item.IsActive);
            modelBuilder.Entity<Category>().HasMany<Equipment>(x => x.EquipmentItems).WithOne(x => x.Category);
            modelBuilder.Entity<Category>().ToTable("RentIt.Categories");

            //ListingImages
            modelBuilder.Entity<ListingImage>().HasQueryFilter(x => x.IsActive);

            //EquipmentImage
            modelBuilder.Entity<EquipmentImage>().HasQueryFilter(x => x.IsActive);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
        }
    }
}
