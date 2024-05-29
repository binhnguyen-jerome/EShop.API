using EShop.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastucture.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var CleanEsential = "ce284d5a-1eba-4967-8d51-1d541a8025d2";
            var DailyId = "91052f64-008c-4f54-a344-154d3f1ce37a";
            var PetId = "09d05933-a638-4694-8c1d-f3e5c998124c";
            // Seed Category data
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = new Guid(CleanEsential), Name = "Clean", Description = "Test" },
                new Category { Id = new Guid(DailyId), Name = "Daily", Description = "Test" },
                new Category { Id = new Guid(PetId), Name = "Pet", Description = "Test" }
            );
            // Seed Data Identity Role 
            var adminRoleId = "cc6b8705-6ce1-4233-8e73-56255932c8cb";
            var customerRoleId = "d6059475-8e7f-42ac-8194-027f5d2a594e";
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
              new IdentityRole<Guid> { Id = new Guid(adminRoleId), Name = "Admin", ConcurrencyStamp = adminRoleId, NormalizedName = "Admin".ToUpper() },
              new IdentityRole<Guid> { Id = new Guid(customerRoleId), Name = "Customer", ConcurrencyStamp = customerRoleId, NormalizedName = "Customer".ToUpper() }
          );
        }
    }
}
