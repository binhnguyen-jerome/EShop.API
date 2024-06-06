using EShop.ViewModels.Dtos.Cart;
using EShop.ViewModels.Dtos.Order;
using EShop.ViewModels.Dtos.User;

namespace EShop.ViewModels.ViewModel
{
    public class CartVM
    {
        public List<CartResponse> CartItems { get; set; }
        public OrderRequest OrderRequest { get; set; }
        public decimal TotalPrice => CartItems.Sum(x => x.Product.PriceDiscount * x.Quantity);

        public static CartVM Create(List<CartResponse> cartItems, OrderRequest orderRequest, UserReponse user)
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
                    City = user.City,
                    OrderTotal = cartItems.Sum(x => x.Product.PriceDiscount * x.Quantity)
                }
            };
        }
        public static CartVM Create(List<CartResponse> cartItems, OrderRequest orderRequest)
        {
            return new CartVM
            {
                CartItems = cartItems,
                OrderRequest = orderRequest
            };
        }
    }
}
