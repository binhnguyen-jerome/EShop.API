using EShop.ViewModels.Dtos.Cart;
using EShop.ViewModels.Dtos.Order;

namespace EShop.ViewModels.ViewModel
{
    public class CartVM
    {
        public List<CartResponse> CartItems { get; set; }
        public OrderRequest OrderRequest { get; set; }
        public decimal TotalPrice => CartItems.Sum(x => x.Product.PriceDiscount * x.Quantity);
    }
}
