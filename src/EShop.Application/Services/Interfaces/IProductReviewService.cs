using EShop.ViewModels.Dtos.Review;

namespace EShop.Application.Services.Interfaces
{
    public interface IProductReviewService
    {
        Task<ProductReviewResponse> CreateProductReviewAsync(ProductReviewRequest productReviewRequest);
        Task<ProductReviewResponse> UpdateProductReviewAsync(Guid id, UpdateProductReviewRequest updateProductReviewRequest);
        Task<bool> DeleteProductReviewAsync(Guid productReviewId);
        Task<List<ProductReviewResponse>> GetProductReviewsAsync(Guid productId);

    }
}
