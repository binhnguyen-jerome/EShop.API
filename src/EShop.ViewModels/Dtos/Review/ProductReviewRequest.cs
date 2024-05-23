namespace EShop.ViewModels.Dtos.Review
{
    public class ProductReviewRequest
    {
        public int Rate { get; set; }
        public string? Content { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        public Guid ProductId { get; set; }

        public Guid ApplicationUserId { get; set; } = new Guid("c810de1a-125e-493b-e06f-08dc762a7bf6");
    }
    public class UpdateProductReviewRequest
    {
        public int Rate { get; set; }
        public string? Content { get; set; }
    }
}
