namespace EShop.ViewModels.Dtos.Cart
{
    public class CartRequest
    {
        public Guid ProductId { get; set; }
        public Guid ApplicationUserId { get; set; }

        public int Quantity { get; set; }
    }
}
