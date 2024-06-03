using System.ComponentModel.DataAnnotations;

namespace EShop.ViewModels.Dtos.Cart
{
    public class CartRequest
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public Guid ApplicationUserId { get; set; }
        [Required]

        public int Quantity { get; set; }
    }
}
