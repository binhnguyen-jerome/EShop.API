namespace EShop.Core.Entities
{
    public class OrderItem : BaseModel
    {
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
