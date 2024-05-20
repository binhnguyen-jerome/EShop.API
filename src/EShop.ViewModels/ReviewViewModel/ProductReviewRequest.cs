namespace EShop.ViewModels.ProductReviewViewModel
{
    public class ProductReviewRequest
    {
        public int Rate { get; set; }
        public string? Content { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        public Guid ProductId { get; set; }

        public Guid ApplicationUserId { get; set; }
    }
    public class UpdateProductReviewRequest
    {
        public int Rate { get; set; }
        public string? Content { get; set; }
    }
}
