using EShop.Core.Entities;

namespace EShop.Core.Repositories
{
    public interface IProductQueries
    {
        Task<List<Product>> GetProductsAsync();

        Task<Product?> GetByIdAsync(Guid id);
    }
}
