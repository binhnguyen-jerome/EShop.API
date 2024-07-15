using EShop.ViewModels.Dtos.Review;

namespace EShop.Application.Services.Interfaces
{
    public interface IProductReviewService
    {
        Task<RatingResponse> CreateProductReviewAsync(RatingRequest ratingRequest);
        Task<RatingResponse> UpdateProductReviewAsync(int id, UpdateRatingRequest updateProductReviewRequest);
        Task<bool> DeleteProductReviewAsync(int productReviewId);
        Task<List<RatingResponse>> GetProductReviewsAsync(int productId);

    }
}
