using EShop.Core.Entities;

namespace EShop.Core.Repositories.Query
{
    public interface ICartQueries
    {
        Task<List<Cart>> GetUserCartsAsync(int applicationUserId);

        Task<Cart?> GetCartByIdAsync(int cartId);

        Task<CartItem?> GetCartItemByUserIdAndProductIdAsync(int applicationUserId, int productId);

        Task<Cart?> GetCartByUserIdAsync(int applicationUserId);
    }

}
