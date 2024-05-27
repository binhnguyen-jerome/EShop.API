using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Infrastucture.Data;
using EShop.ViewModels.Dtos.Product;
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
                .Include(p => p.ProductReviews)
                .Include(p => p.ProductImages)
                .ThenInclude(p => p.Image)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetFilteredProductsAsync(ProductQuery query)
        {
            var data = dbSet.AsQueryable();
            if (query.CategoryId != null)
            {
                data = data.Where(p => p.CategoryId == query.CategoryId);
            }
            if (query.MinPrice != null)
            {
                data = data.Where(p => p.Price >= query.MinPrice);
            }
            if (query.MaxPrice != null)
            {
                data = dbSet.Where(p => p.Price <= query.MaxPrice);
            }
            return await data
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .ThenInclude(p => p.Image)
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
        }
    }
}
