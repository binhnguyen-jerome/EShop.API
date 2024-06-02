using EShop.ViewModels.Dtos.Product;

namespace EShop.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetProductsAsync(ProductQuery query);
        Task<ProductResponse?> GetProductByIdAsync(Guid id);
        Task<ProductResponse> CreateProductAsync(CreateProductRequest? product);
        Task<ProductResponse> UpdateProductAsync(Guid id, UpdateProductRequest? product);
        Task<bool> DeleteProductAsync(Guid id);
    }
}
