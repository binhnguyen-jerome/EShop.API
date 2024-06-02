using EShop.Core.Domain.Entities;
using EShop.ViewModels.Dtos.Cart;
using EShop.ViewModels.Dtos.Product;

namespace EShop.Application.Mappers
{
    public static class CartMapper
    {
        public static Cart ToAddToCart(this CartRequest cartRequest)
        {
            return new Cart
            {
                ProductId = cartRequest.ProductId,
                Quantity = cartRequest.Quantity,
                ApplicationUserId = cartRequest.ApplicationUserId
            };
        }
        public static CartResponse ToCartResponse(this Cart cart)
        {
            return new CartResponse
            {
                Id = cart.Id,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
                ApplicationUserId = cart.ApplicationUserId,
                Product = new ProductResponse
                {
                    Id = cart.Product.Id,
                    Name = cart.Product.Name,
                    Price = cart.Product.Price,
                    ProductImages = cart.Product.ProductImages.Select(pi => new ProductImageResponse
                    {
                        ImageUrl = pi.ImageUrl
                    }).ToList(),
                }
            };
        }
    }
}
