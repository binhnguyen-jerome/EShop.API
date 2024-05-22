using EShop.ViewModels.ProductReviewViewModel;

namespace EShop.CustomerFe.Services.Interfaces
{
    public interface IProductReviewService
    {
        Task<List<ProductReviewUserResponse>> GetProductReviewsAsync(Guid productId);
        Task<ProductReviewResponse> CreateProductReviewAsync(ProductReviewRequest request);
    }
}
