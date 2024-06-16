namespace EShop.Core.Entities
{
    public class Cart : BaseModel
    {
        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

        public int Quantity { get; set; }
    }
}
