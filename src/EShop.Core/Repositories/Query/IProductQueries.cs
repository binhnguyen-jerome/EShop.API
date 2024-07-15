using EShop.Core.Entities;

namespace EShop.Core.Repositories.Query
{
    public interface IProductQueries
    {
        Task<List<Product>> GetProductsAsync();

        Task<Product?> GetByIdAsync(int id);
    }
}
