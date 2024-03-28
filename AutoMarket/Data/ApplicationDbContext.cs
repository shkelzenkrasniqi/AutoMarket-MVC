using AutoMarket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarPhoto> Photos { get; set; }
        public DbSet<MotorcyclePhoto> MotorcyclePhotos { get; set; }
        public DbSet<CarBrand> Brands { get; set; }
        public DbSet<CarModel> Models { get; set; }
        public DbSet<Motorcycle>Motorcycles { get; set; }
        public DbSet<MotorcycleBrand> MotorcycleBrands { get; set; }
        public DbSet<MotorcycleModel> MotorcycleModels { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarBrand>()
                   .HasMany(cb => cb.CarModels)
                   .WithOne(cm => cm.CarBrand)
                   .HasForeignKey(cm => cm.CarBrandId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CarModel>()
                .HasOne(cm => cm.CarBrand)
                   .WithMany(cb => cb.CarModels)
                   .HasForeignKey(cm => cm.CarBrandId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MotorcycleBrand>()
                   .HasMany(mb => mb.MotorcycleModels)
                   .WithOne(mm => mm.MotorcycleBrand)
                   .HasForeignKey(mm => mm.MotorcycleBrandId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MotorcycleModel>()
                .HasOne(mm => mm.MotorcycleBrand)
                .WithMany(mb => mb.MotorcycleModels)
                .HasForeignKey(mm => mm.MotorcycleBrandId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}