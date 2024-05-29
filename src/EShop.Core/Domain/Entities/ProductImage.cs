namespace EShop.Core.Domain.Entities
{
    public class ProductImage : BaseModel
    {
        public string ImageUrl { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
