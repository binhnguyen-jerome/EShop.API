using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Infrastucture.Data;
using EShop.ViewModels.Dtos.Review;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastucture.Repositories
{
    public class ProductReviewQueries : BaseQuery<ProductReview>, IProductReviewQueries
    {
        public ProductReviewQueries(ApplicationDbContext db) : base(db)
        {
        }
        public async Task<List<ProductReview>> GetFilteredProductReviewsAsync(ProductReviewQuery query)
        {
            var queryable = dbSet.AsQueryable();
            if (query.ProductId.HasValue)
            {
                queryable = queryable.Where(p => p.ProductId == query.ProductId);
            }
            return await queryable
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .Include(p => p.ApplicationUser)
                .ToListAsync();
        }
    }
}
