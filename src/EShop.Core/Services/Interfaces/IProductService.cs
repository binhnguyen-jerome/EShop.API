using EShop.ViewModels.ProductViewModel;

namespace EShop.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task<List<ProductResponse>> GetProductsByCategoryAsync(Guid? categoryId);
        Task<ProductResponse?> GetProductByIdAsync(Guid? id);
        Task<ProductResponse> CreateProductAsync(CreateProductRequest? product);
        Task<ProductResponse> UpdateProductAsync(Guid? id, UpdateProductRequest? product);
        Task<bool> DeleteProductAsync(Guid? id);
    }
}
