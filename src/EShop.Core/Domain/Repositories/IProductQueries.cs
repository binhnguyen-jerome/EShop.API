using EShop.Core.Domain.Entities;
using EShop.ViewModels.Dtos.Product;

namespace EShop.Core.Domain.Repositories
{
    public interface IProductQueries
    {
        Task<List<Product>> GetFilteredProductsAsync(ProductQuery query);

        Task<List<Product>> GetAllProductByCategoryAsync(Guid categoryId);
        Task<Product?> GetByIdAsync(Guid id);
    }
}
