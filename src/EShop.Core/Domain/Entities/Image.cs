namespace EShop.Core.Domain.Entities
{
    public class Image : BaseModel
    {
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductReviewImage> ProductReviewImages { get; set; }
    }
}
