using EShop.ViewModels.Dtos.CartItem;
using EShop.ViewModels.Dtos.Product;

namespace EShop.ViewModels.Dtos.Cart
{
    public class CartResponse
    {
        public int Id { get; set; }

        public int ApplicationUserId { get; set; }
        
        public ICollection<CartItemResponse> CartItems { get; set; } = new List<CartItemResponse>();

    }
}
