using EShop.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastucture.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            var MenShirtId = "ce284d5a-1eba-4967-8d51-1d541a8025d2";
            var MenPantId = "91052f64-008c-4f54-a344-154d3f1ce37a";
            var AccessoryId = "09d05933-a638-4694-8c1d-f3e5c998124c";
            // Seed Category data
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = new Guid(MenShirtId), Name = "Men Shirt", Description = "Test" },
                new Category { Id = new Guid(MenPantId), Name = "Men Pant", Description = "Test" },
                new Category { Id = new Guid(AccessoryId), Name = "Accessory", Description = "Test" }
            );
        }
    }
}
