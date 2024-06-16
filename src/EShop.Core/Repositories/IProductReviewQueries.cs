using EShop.Core.Entities;

namespace EShop.Core.Repositories
{
    public interface IProductReviewQueries
    {
        Task<List<ProductReview>> GetFilteredProductReviewsAsync(Guid productId);
    }
}
