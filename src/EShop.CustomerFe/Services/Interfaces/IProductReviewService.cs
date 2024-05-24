using EShop.ViewModels.Dtos.Review;

namespace EShop.CustomerFe.Services.Interfaces
{
    public interface IProductReviewService
    {
        Task<List<ProductReviewUserResponse>> GetProductReviewsAsync(Guid productId);
        Task<ProductReviewResponse> CreateProductReviewAsync(ProductReviewRequest request);
    }
}
