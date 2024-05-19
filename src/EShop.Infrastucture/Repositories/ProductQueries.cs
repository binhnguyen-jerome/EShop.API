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

        public async Task<List<Product>> GetAllProductByCategoryAsync(Guid categoryId)
        {
            return await dbSet
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .ThenInclude(p => p.Image)
                .ToListAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await dbSet
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .ThenInclude(p => p.Image)
                .ToListAsync();
        }
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await dbSet
                .Where(p => p.Id == id)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .ThenInclude(p => p.Image)
                .FirstOrDefaultAsync();
        }
    }
}
