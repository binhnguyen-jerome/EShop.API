using EShop.Core.Entities;

namespace EShop.Core.Repositories
{
    public interface ICartQueries
    {
        Task<List<Cart>> GetUserCartsAsync(Guid applicationUserId);

        Task<Cart?> GetByIdAsync(Guid id);

        Task<Cart?> GetCartByUserIdAndProductIdAsync(Guid applicationUserId, Guid productId);
    }
}
