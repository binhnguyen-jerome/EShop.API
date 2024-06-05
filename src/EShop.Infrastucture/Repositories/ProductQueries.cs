using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastucture.Repositories
{
    public class ProductQueries : BaseQuery<Product>, IProductQueries
    {
        public ProductQueries(ApplicationDbContext db) : base(db)
        { }

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
