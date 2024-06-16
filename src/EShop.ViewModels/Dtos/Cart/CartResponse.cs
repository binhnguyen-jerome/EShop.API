using EShop.ViewModels.Dtos.Product;

namespace EShop.ViewModels.Dtos.Cart
{
    public class CartResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        public Guid ApplicationUserId { get; set; }
        public int Quantity { get; set; }

        public ProductResponse Product { get; set; }
    }
}
