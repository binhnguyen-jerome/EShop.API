using EShop.Core.Domain.Entities;

namespace EShop.Core.Domain.Repositories
{
    public interface IProductReviewQueries
    {
        Task<List<ProductReview>> GetFilteredProductReviewsAsync(Guid productId);
    }
}
