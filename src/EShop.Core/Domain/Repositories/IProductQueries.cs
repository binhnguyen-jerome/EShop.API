using EShop.Core.Domain.Entities;

namespace EShop.Core.Domain.Repositories
{
    public interface IProductQueries
    {
        Task<List<Product>> GetAllProductsAsync();

        Task<List<Product>> GetAllProductByCategoryAsync(Guid categoryId);
        Task<Product?> GetByIdAsync(Guid id);
    }
}
