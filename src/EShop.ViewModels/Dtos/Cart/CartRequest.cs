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
        public static CartRequest Create(string userId, Guid productId, int quantity)
        {
            return new CartRequest
            {
                ApplicationUserId = new Guid(userId),
                ProductId = productId,
                Quantity = quantity
            };
        }
    }
}
