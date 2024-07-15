using EShop.Core.Entities;
using EShop.Core.Repositories;
using EShop.Core.Repositories.Query;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastucture.Repositories.Query
{
    public class ProductQueries(ApplicationDbContext db) : BaseQuery<Product>(db), IProductQueries
    {
        public async Task<Product?> GetByIdAsync(int id)
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
