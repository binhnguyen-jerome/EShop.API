using EShop.ViewModels.Dtos.Cart;

namespace EShop.CustomerFe.Services.Interfaces
{
    public interface ICartClientService
    {
        Task<List<CartResponse>?> GetCartByUserIdAsync(int userId);
        Task<CartResponse?> AddToCartAsync(CartRequest cartRequest);
        Task<bool> RemoveFromCartAsync(int cartId);

        Task<bool> UpdateCartAsync(CartRequest cartRequest);
    }
}
