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
        public async Task<CartResponse> AddToCartAsync(CartRequest cartRequest)
        {
            if (cartRequest.Quantity <= 0)
                throw new ApplicationException("Quantity must be greater than 0");
            Cart cart;
            var existingCart = await cartQueries.GetCartByUserIdAndProductIdAsync(cartRequest.ApplicationUserId, cartRequest.ProductId);
            if (existingCart != null)
            {
                existingCart.Quantity += cartRequest.Quantity;
                cartRepository.Update(existingCart);
                cart = await cartRepository.Get(c => c.Id == existingCart.Id);
            }
            else
            {
                cart = cartRequest.ToAddToCart();
                cartRepository.Add(cart);
            }
            await unitOfWork.CompleteAsync();
            return cart.ToCartResponse();
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
            if (cart.Quantity <= 0)
            {
                cartRepository.Remove(cart);
                await unitOfWork.CompleteAsync();
                return true;
            }
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
