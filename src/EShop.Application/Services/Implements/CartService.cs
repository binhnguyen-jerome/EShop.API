using EShop.Application.Mappers;
using EShop.Application.Services.Interfaces;
using EShop.Core.Entities;
using EShop.Core.Exceptions;
using EShop.Core.Repositories;
using EShop.ViewModels.Dtos.Cart;

namespace EShop.Application.Services.Implements
{
    public class CartService(IUnitOfWork unitOfWork, ICartQueries cartQueries) : ICartService
    {
        private readonly IGenericRepository<Cart> cartRepository = unitOfWork.GetBaseRepo<Cart>();

        public async Task<CartResponse> AddToCartAsync(CartRequest cartRequest)
        {
            if (cartRequest.Quantity <= 0)
                throw new ApplicationException("Quantity must be greater than 0");

            var existingCart = await cartQueries.GetCartByUserIdAndProductIdAsync(cartRequest.ApplicationUserId, cartRequest.ProductId);

            var cart = existingCart != null ? await UpdateExistingCartAsync(existingCart, cartRequest.Quantity) : await CreateNewCartAsync(cartRequest);

            await unitOfWork.CompleteAsync();
            if (cart != null) return cart.ToCartResponse();
            throw new ApplicationException("Error adding to cart");
        }

        private async Task<Cart?> UpdateExistingCartAsync(Cart existingCart, int quantity)
        {
            existingCart.Quantity += quantity;
            cartRepository.Update(existingCart);
            var cart = await cartRepository.GetAsync(c => c.Id == existingCart.Id);
            return cart;
        }

        private Task<Cart> CreateNewCartAsync(CartRequest cartRequest)
        {
            var cart = cartRequest.ToAddToCart();
            cartRepository.Add(cart);
            return Task.FromResult(cart);
        }
        public async Task<List<CartResponse>> GetUserCartsAsync(Guid applicationUserId)
        {
            var userCarts = await cartQueries.GetUserCartsAsync(applicationUserId);
            return userCarts.Select(c => c.ToCartResponse()).ToList();
        }

        public async Task<bool> UpdateCartAsync(CartRequest cartRequest)
        {
            if (cartRequest.Quantity <= 0)
                throw new ApplicationException("Quantity must be greater than 0");

            var cart = await cartQueries.GetCartByUserIdAndProductIdAsync(cartRequest.ApplicationUserId, cartRequest.ProductId).ThrowIfNull("Product In Cart Not Found");

            cart.Quantity = cartRequest.Quantity;
            cartRepository.Update(cart);
            await unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> RemoveFromCartAsync(Guid cartId)
        {
            var cart = await cartRepository.GetAsync(c => c.Id == cartId).ThrowIfNull($"Cart {cartId} not founded");
            cartRepository.Remove(cart);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}
