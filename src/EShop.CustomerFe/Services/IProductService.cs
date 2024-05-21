using EShop.ViewModels.ProductViewModel;

namespace EShop.CustomerFe.Services
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task<ProductResponse> GetProductByIdAsync(Guid id);

        Task<List<ProductResponse>> GetProductByCategoryIdAsync(Guid categoryId);
    }
}
