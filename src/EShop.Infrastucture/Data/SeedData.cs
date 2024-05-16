using EShop.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastucture.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var MenShirtId = "ce284d5a-1eba-4967-8d51-1d541a8025d2";
            var MenPantId = "91052f64-008c-4f54-a344-154d3f1ce37a";
            var AccessoryId = "09d05933-a638-4694-8c1d-f3e5c998124c";
            // Seed Category data
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = new Guid(MenShirtId), Name = "Men Shirt", Description = "Test" },
                new Category { Id = new Guid(MenPantId), Name = "Men Pant", Description = "Test" },
                new Category { Id = new Guid(AccessoryId), Name = "Accessory", Description = "Test" }
            );
            //Seed Product data
            var Product1Id = "b0d83c5a-1b71-4e1c-97ec-c59de6bc5c67";
            var Product2Id = "15256d5c-9038-4e98-89f2-664010a847d8";
            modelBuilder.Entity<Product>().HasData(
                 new Product { Id = new Guid(Product1Id), Name = "Shirt12", Description = "Shirt12", Summary = "Shirt12", CategoryId = new Guid(MenShirtId), CreateDate = DateTime.Now, Price = 200, Stock = 12 },
                  new Product { Id = new Guid(Product2Id), Name = "Shirt13", Description = "Shirt13", Summary = "Shirt13", CategoryId = new Guid(MenShirtId), CreateDate = DateTime.Now, Price = 300, Stock = 100 }
                 ); ;
        }
    }
}
