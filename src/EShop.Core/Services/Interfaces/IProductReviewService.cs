using EShop.ViewModels.Dtos.Review;

namespace EShop.Core.Services.Interfaces
{
    public interface IProductReviewService
    {
        Task<ProductReviewResponse> CreateProductReviewAsync(ProductReviewRequest? productReviewRequest);
        Task<ProductReviewResponse> UpdateProductReviewAsync(Guid? id, UpdateProductReviewRequest? updateProductReviewRequest);
        Task<bool> DeleteProductReviewAsync(Guid? productReviewId);
        Task<List<ProductReviewResponse>> GetProductReviewsByProductIdAsync(Guid? productId);

    }
}
