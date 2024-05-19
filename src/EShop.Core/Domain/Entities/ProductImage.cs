namespace EShop.Core.Domain.Entities
{
    public class ProductImage : BaseModel
    {
        public Guid ImageId { get; set; }
        public Guid ProductId { get; set; }
        public virtual Image Image { get; set; }
        public virtual Product Product { get; set; }
    }
}
