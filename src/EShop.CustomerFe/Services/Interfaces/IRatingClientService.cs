using EShop.ViewModels.Dtos.Review;

namespace EShop.CustomerFe.Services.Interfaces
{
    public interface IRatingClientService
    {
        Task<List<RatingUserResponse>?> GetProductReviewsAsync(int productId);
        Task<RatingResponse?> CreateProductReviewAsync(RatingRequest request);
    }
}
