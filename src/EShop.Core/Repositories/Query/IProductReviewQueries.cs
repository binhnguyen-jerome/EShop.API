using EShop.Core.Entities;

namespace EShop.Core.Repositories.Query
{
    public interface IProductReviewQueries
    {
        Task<List<Rating>> GetFilteredProductReviewsAsync(int productId);
    }
}
