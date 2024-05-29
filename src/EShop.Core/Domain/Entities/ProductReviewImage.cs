namespace EShop.Core.Domain.Entities
{
    public class ProductReviewImage : BaseModel
    {
        public string ImageUrl { get; set; }

        public Guid ProductReviewId { get; set; }
        public virtual ProductReview ProductReview { get; set; }

    }
}
