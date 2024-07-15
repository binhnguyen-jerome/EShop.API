using EShop.ViewModels.Dtos.Cart;
using EShop.ViewModels.Dtos.CartItem;
using EShop.ViewModels.Dtos.Order;
using EShop.ViewModels.Dtos.User;

namespace EShop.ViewModels.ViewModel
{
    public class CartVM
    {
        public List<CartItemResponse> CartItems { get; set; }
        public OrderRequest OrderRequest { get; set; }
        public decimal TotalPrice => CartItems.Sum(x => x.Product.PriceDiscount * x.Quantity);

        public static CartVM Create(List<CartItemResponse> cartItems, OrderRequest orderRequest, UserReponse user)
        {
            return new CartVM
            {
                CartItems = cartItems,
                OrderRequest = new OrderRequest
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    StreetAddress = user.StreetAddress,
                    PostalCode = user.PostalCode,
                    State = user.State,
                    City = user.City,
                    OrderTotal = cartItems.Sum(x => x.Product.PriceDiscount * x.Quantity)
                }
            };
        }
        public static CartVM Create(List<CartItemResponse> cartItems, OrderRequest orderRequest)
        {
            return new CartVM
            {
                CartItems = cartItems,
                OrderRequest = orderRequest
            };
        }
    }
}
