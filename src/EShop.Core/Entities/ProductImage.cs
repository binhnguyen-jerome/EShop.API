namespace EShop.Core.Entities
{
    public class ProductImage : BaseModel
    {
        public string ImageUrl { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
