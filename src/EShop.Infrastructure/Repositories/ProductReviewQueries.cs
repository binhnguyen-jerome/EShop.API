using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Repositories
{
    public class ProductReviewQueries(ApplicationDbContext db) : BaseQuery<ProductReview>(db), IProductReviewQueries
    {
        public async Task<List<ProductReview>> GetFilteredProductReviewsAsync(Guid productId)
        {
            var queryable = dbSet.AsQueryable();
            return await queryable
                .Where(p => p.ProductId == productId)
                .Include(p => p.ApplicationUser)
                .ToListAsync();
        }
    }
}
