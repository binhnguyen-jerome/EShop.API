using EShop.Application.Mappers;
using EShop.Application.Services.Interfaces;
using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.ViewModels.Dtos.Cart;

namespace EShop.Application.Services.Implements
{
    public class CartService : ICartService
    {
        private readonly IGenericRepository<Cart> cartRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICartQueries cartQueries;

        public CartService(IUnitOfWork unitOfWork, ICartQueries cartQueries)
        {
            this.unitOfWork = unitOfWork;
            this.cartQueries = cartQueries;
            cartRepository = unitOfWork.GetBaseRepo<Cart>();

        }
        public async Task<CartResponse> AddToCartAsync(CartRequest? cartRequest)
        {
            if (cartRequest == null)
                throw new ArgumentNullException(nameof(cartRequest));
            if (cartRequest.Quantity <= 0)
                throw new ApplicationException("Quantity must be greater than 0");
            Cart cart = cartRequest.ToAddToCart();
            cartRepository.Add(cart);
            await unitOfWork.CompleteAsync();
            var newCart = await cartQueries.GetByIdAsync(cart.Id);
            return newCart.ToCartResponse();
        }

        public async Task<List<CartResponse>> GetUserCartsAsync(Guid applicationUserId)
        {
            var userCarts = await cartQueries.GetUserCartsAsync(applicationUserId);
            return userCarts.Select(c => c.ToCartResponse()).ToList();
        }

        public async Task<bool> MinusAsync(Guid cartId)
        {
            var cart = await cartRepository.Get(c => c.Id == cartId);
            if (cart == null)
                throw new KeyNotFoundException("Cart not found");
            cart.Quantity--;
            cartRepository.Update(cart);
            await unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> PlusAsync(Guid cartId)
        {
            var cart = await cartRepository.Get(c => c.Id == cartId);
            if (cart == null)
                throw new KeyNotFoundException("Cart not found");
            cart.Quantity++;
            cartRepository.Update(cart);
            await unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> RemoveFromCartAsync(Guid cartId)
        {
            var cart = await cartRepository.Get(c => c.Id == cartId);
            if (cart == null)
                throw new KeyNotFoundException("Cart not found");
            cartRepository.Remove(cart);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}
