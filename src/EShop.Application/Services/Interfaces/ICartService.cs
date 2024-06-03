using EShop.ViewModels.Dtos.Cart;

namespace EShop.Application.Services.Interfaces
{
    public interface ICartService
    {
        Task<List<CartResponse>> GetUserCartsAsync(Guid applicationUserId);
        Task<CartResponse> AddToCartAsync(CartRequest cartRequest);
        Task<bool> RemoveFromCartAsync(Guid cartId);

        Task<bool> MinusAsync(Guid cartId);
        Task<bool> PlusAsync(Guid cartId);
    }
}
