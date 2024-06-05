using EShop.ViewModels.Dtos.Cart;

namespace EShop.CustomerFe.Services.Interfaces
{
    public interface ICartService
    {
        Task<List<CartResponse>> GetCartByUserIdAsync(Guid userId);
        Task<CartResponse> AddToCartAsync(CartRequest cartRequest);
        Task<bool> RemoveFromCartAsync(Guid cartId);

        Task<bool> UpdateCartAsync(CartRequest cartRequest);
    }
}
