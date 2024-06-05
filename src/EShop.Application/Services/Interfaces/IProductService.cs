using EShop.ViewModels.Dtos.Product;

namespace EShop.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetProductsAsync();
        Task<ProductResponse> GetProductByIdAsync(Guid id);
        Task<bool> CreateProductAsync(CreateProductRequest product);
        Task<ProductResponse> UpdateProductAsync(Guid id, UpdateProductRequest product);
        Task<bool> DeleteProductAsync(Guid id);
    }
}
