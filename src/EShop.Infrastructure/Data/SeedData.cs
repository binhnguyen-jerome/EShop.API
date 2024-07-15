using EShop.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed Category data
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Clean", Description = "Test" },
                new Category { Id = 2, Name = "Daily", Description = "Test" },
                new Category { Id = 3, Name = "Pet", Description = "Test" }
            );
            // Seed Product data
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Description = "Test product 1",
                    Summary = "Summary of product 1",
                    Price = 100,
                    PriceDiscount = 90,
                    Stock = 10,
                    CreateDate = DateTime.Now,
                    UpdateDate = null,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Product 2",
                    Description = "Test product 2",
                    Summary = "Summary of product 2",
                    Price = 200,
                    PriceDiscount = 180,
                    Stock = 20,
                    CreateDate = DateTime.Now,
                    UpdateDate = null,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 3,
                    Name = "Product 3",
                    Description = "Test product 3",
                    Summary = "Summary of product 3",
                    Price = 300,
                    PriceDiscount = 270,
                    Stock = 30,
                    CreateDate = DateTime.Now,
                    UpdateDate = null,
                    CategoryId = 3
                }
            );
            // Seed Data Identity Role 
            modelBuilder.Entity<IdentityRole<int>>().HasData(
              new IdentityRole<int> { Id = 1, Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin".ToUpper() },
              new IdentityRole<int> { Id = 2, Name = "Customer", ConcurrencyStamp = "2", NormalizedName = "Customer".ToUpper() }
          );
        }
    }
}
