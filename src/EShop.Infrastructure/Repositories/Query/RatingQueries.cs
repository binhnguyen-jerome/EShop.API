using EShop.Core.Entities;
using EShop.Core.Repositories;
using EShop.Core.Repositories.Query;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastucture.Repositories.Query
{
    public class RatingQueries(ApplicationDbContext db) : BaseQuery<Rating>(db), IProductReviewQueries
    {
        public async Task<List<Rating>> GetFilteredProductReviewsAsync(int productId)
        {
            var queryable = dbSet.AsQueryable();
            return await queryable
                .Where(p => p.ProductId == productId)
                .Include(p => p.ApplicationUser)
                .ToListAsync();
        }
    }
}
