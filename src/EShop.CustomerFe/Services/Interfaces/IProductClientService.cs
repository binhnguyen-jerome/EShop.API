using EShop.ViewModels.Dtos.Product;

namespace EShop.CustomerFe.Services.Interface
{
    public interface IProductClientService
    {
        Task<List<ProductResponse>?> GetAllProductsAsync();
        Task<ProductResponse?> GetProductByIdAsync(Guid productId);

        Task<List<ProductResponse>?> GetFilterProducts(ProductQuery query);
    }
}
