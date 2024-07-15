using System.ComponentModel.DataAnnotations;
using EShop.ViewModels.Dtos.CartItem;

namespace EShop.ViewModels.Dtos.Cart
{
    public class CartRequest
    {
        [Required]
        public int ApplicationUserId { get; set; }

        [Required]
        public List<CartItemRequest> CartItems { get; set; } = new List<CartItemRequest>();

        public static CartRequest Create(int userId, List<CartItemRequest> cartItemsRequest)
        {
            return new CartRequest
            {
                ApplicationUserId = userId,
                CartItems = cartItemsRequest
            };
        }
    }
}
