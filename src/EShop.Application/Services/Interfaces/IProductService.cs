using EShop.ViewModels.Dtos.Product;

namespace EShop.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetProductsAsync();
        Task<ProductResponse> GetProductByIdAsync(int id);
        Task<bool> CreateProductAsync(CreateProductRequest product);
        Task<ProductResponse> UpdateProductAsync(int id, UpdateProductRequest product);
        Task<bool> DeleteProductAsync(int id);
    }
}
