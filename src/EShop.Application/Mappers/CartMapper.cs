using EShop.Core.Entities;
using EShop.ViewModels.Dtos.Cart;
using EShop.ViewModels.Dtos.CartItem;
using EShop.ViewModels.Dtos.Product;

namespace EShop.Application.Mappers
{
    public static class CartMapper
    {
        public static Cart ToAddToCart(this CartRequest cartRequest)
        {
            return new Cart
            {
                ApplicationUserId = cartRequest.ApplicationUserId,
                CartItems = cartRequest.CartItems.Select(cartItemRequest => new CartItem
                {
                    ProductId = cartItemRequest.ProductId,
                    Quantity = cartItemRequest.Quantity
                }).ToList()
            };
        }

        public static CartResponse ToCartResponse(this Cart cart)
        {
            return new CartResponse
            {
                Id = cart.Id,
                ApplicationUserId = cart.ApplicationUserId,
                CartItems = cart.CartItems.Select(cartItem => new CartItemResponse
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Product = new ProductResponse
                    {
                        Id = cartItem.Product.Id,
                        Name = cartItem.Product.Name,
                        Price = cartItem.Product.Price,
                        PriceDiscount = cartItem.Product.PriceDiscount,
                        Description = cartItem.Product.Description,
                        ProductImages = cartItem.Product.ProductImages.Select(pi => new ProductImageResponse
                        {
                            ImageUrl = pi.ImageUrl
                        }).ToList(),
                    }
                }).ToList()
            };
        }
    }

}
