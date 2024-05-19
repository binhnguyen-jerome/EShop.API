namespace EShop.Core.Domain.Entities
{
    public class ProductReviewImage : BaseModel
    {
        public Guid ImageId { get; set; }
        public virtual Image Image { get; set; }

        public Guid ProductReviewId { get; set; }
        public virtual ProductReview ProductReview { get; set; }

    }
}
