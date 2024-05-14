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
    }
}
