using EShop.Core.Entities;
using EShop.Core.Repositories;
using EShop.Infrastructure.Data;
using EShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Repositories
{
    public class ProductQueries(ApplicationDbContext db) : BaseQuery<Product>(db), IProductQueries
    {
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await dbSet
                .Where(p => p.Id == id)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await dbSet
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .ToListAsync();
        }
    }
}
