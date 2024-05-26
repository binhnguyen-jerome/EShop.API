using EShop.Core.Domain.Entities;
using EShop.ViewModels.Dtos.Review;

namespace EShop.Core.Domain.Repositories
{
    public interface IProductReviewQueries
    {
        Task<List<ProductReview>> GetFilteredProductReviewsAsync(ProductReviewQuery query);
    }
}
