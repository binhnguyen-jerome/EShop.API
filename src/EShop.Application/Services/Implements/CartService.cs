using EShop.Application.Mappers;
using EShop.Application.Services.Interfaces;
using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Extensions;
using EShop.Core.Domain.Repositories;
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
            Cart cart;
            var existingCart = await cartQueries.GetCartByUserIdAndProductIdAsync(cartRequest.ApplicationUserId, cartRequest.ProductId);
            if (existingCart != null)
            {
                existingCart.Quantity += cartRequest.Quantity;
                cartRepository.Update(existingCart);
                cart = await cartRepository.GetAsync(c => c.Id == existingCart.Id);
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
