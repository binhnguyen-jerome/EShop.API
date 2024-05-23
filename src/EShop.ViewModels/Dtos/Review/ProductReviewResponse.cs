namespace EShop.ViewModels.Dtos.Review
{
    public class ProductReviewResponse
    {
        public Guid Id { get; set; }
        public int Rate { get; set; }
        public string? Content { get; set; }
        public DateTime CreateAt { get; set; }

        public Guid ProductId { get; set; }
        public Guid ApplicationUserId { get; set; }
    }
    public class ProductReviewUserResponse : ProductReviewResponse
    {
        public string ApplicationUserName { get; set; }
    }
}
