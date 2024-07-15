using EShop.ViewModels.Dtos.Product;

namespace EShop.ViewModels.Dtos.CartItem;

public class CartItemResponse
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public ProductResponse Product { get; set; }
}