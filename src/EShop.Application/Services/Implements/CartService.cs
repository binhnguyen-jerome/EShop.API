using EShop.Application.Mappers;
using EShop.Application.Services.Interfaces;
using EShop.Core.Entities;
using EShop.Core.Exceptions;
using EShop.Core.Repositories.Generic;
using EShop.Core.Repositories.Query;
using EShop.ViewModels.Dtos.Cart;

namespace EShop.Application.Services.Implements
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICartQueries cartQueries;
        private readonly IGenericRepository<Cart> cartRepository;
        private readonly IGenericRepository<CartItem> cartItemRepository;

        public CartService(IUnitOfWork unitOfWork, ICartQueries cartQueries)
        {
            this.unitOfWork = unitOfWork;
            this.cartQueries = cartQueries;
            this.cartRepository = unitOfWork.GetBaseRepo<Cart>();
            this.cartItemRepository = unitOfWork.GetBaseRepo<CartItem>();
        }

        public async Task<CartResponse> AddToCartAsync(CartRequest cartRequest)
        {
            foreach (var item in cartRequest.CartItems)
            {
                if (item.Quantity <= 0)
                    throw new ApplicationException("Quantity must be greater than 0");
            }

            var existingCart = await cartQueries.GetCartByUserIdAsync(cartRequest.ApplicationUserId);
            if (existingCart == null)
            {
                existingCart = await CreateNewCartAsync(cartRequest.ApplicationUserId);
            }

            foreach (var item in cartRequest.CartItems)
            {
                var existingCartItem = existingCart.CartItems.FirstOrDefault(ci => ci.ProductId == item.ProductId);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += item.Quantity;
                    cartItemRepository.Update(existingCartItem);
                }
                else
                {
                    var newCartItem = new CartItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Id = existingCart.Id
                    };
                    existingCart.CartItems.Add(newCartItem);
                    cartItemRepository.Add(newCartItem);
                }
            }

            await unitOfWork.CompleteAsync();
            return existingCart.ToCartResponse();
        }

        private async Task<Cart> CreateNewCartAsync(int applicationUserId)
        {
            var newCart = new Cart
            {
                ApplicationUserId = applicationUserId
            };
            cartRepository.Add(newCart);
            await unitOfWork.CompleteAsync();
            return newCart;
        }

        public async Task<List<CartResponse>> GetUserCartsAsync(int applicationUserId)
        {
            var userCarts = await cartQueries.GetUserCartsAsync(applicationUserId);
            return userCarts.Select(c => c.ToCartResponse()).ToList();
        }
        
        public async Task<bool> UpdateCartAsync(CartRequest cartRequest)
        {
            foreach (var item in cartRequest.CartItems)
            {
                if (item.Quantity <= 0)
                    throw new ApplicationException("Quantity must be greater than 0");
            }

            var cart = await cartQueries.GetCartByUserIdAsync(cartRequest.ApplicationUserId);
            if (cart == null)
                throw new ApplicationException("Cart not found");

            foreach (var item in cartRequest.CartItems)
            {
                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == item.ProductId);
                if (cartItem == null)
                    throw new ApplicationException("Product in cart not found");

                cartItem.Quantity = item.Quantity;
                cartItemRepository.Update(cartItem);
            }

            await unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> RemoveFromCartAsync(int cartId, int productId)
        {
            var cart = await cartRepository.GetAsync(c => c.Id == cartId).ThrowIfNull($"Cart {cartId} not found");
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem == null)
                throw new ApplicationException("Product in cart not found");

            cart.CartItems.Remove(cartItem);
            cartItemRepository.Remove(cartItem);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}