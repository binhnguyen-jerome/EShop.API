using EShop.ViewModels.Dtos.Cart;

namespace EShop.Application.Services.Interfaces
{
    public interface ICartService
    {
        Task<List<CartResponse>> GetUserCartsAsync(int applicationUserId);
        Task<CartResponse> AddToCartAsync(CartRequest cartRequest);
        Task<bool> RemoveFromCartAsync(int applicationUserId, int productId);
        Task<bool> UpdateCartAsync(CartRequest cartRequest);
    }

}
