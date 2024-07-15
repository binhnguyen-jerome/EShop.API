using System.ComponentModel.DataAnnotations;

namespace EShop.ViewModels.Dtos.CartItem;

public class CartItemRequest
{
    [Required]
    public int ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }
}