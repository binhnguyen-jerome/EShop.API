namespace EShop.Core.Domain.Entities
{
    public class ProductReview : BaseModel
    {
        public int Rate { get; set; }
        public string? Content { get; set; }
        public DateTime CreateAt { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual ICollection<ProductReviewImage>? ProductReviewImages { get; set; }
    }
}
