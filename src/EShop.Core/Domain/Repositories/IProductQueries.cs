using EShop.Core.Domain.Entities;

namespace EShop.Core.Domain.Repositories
{
    public interface IProductQueries
    {
        Task<List<Product>> GetProductsAsync();

        Task<Product?> GetByIdAsync(Guid id);
    }
}
