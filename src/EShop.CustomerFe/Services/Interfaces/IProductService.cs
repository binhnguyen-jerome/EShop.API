using EShop.ViewModels.ProductViewModel;

namespace EShop.CustomerFe.Services.Interface
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task<ProductResponse> GetProductByIdAsync(Guid productId);

        Task<List<ProductResponse>> GetProductByCategoryIdAsync(Guid categoryId);
    }
}
