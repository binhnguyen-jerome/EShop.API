using System.ComponentModel.DataAnnotations;

namespace EShop.ViewModels.Dtos.Review
{
    public class ProductReviewRequest
    {
        [Required]
        public int Rate { get; set; }
        public string? Content { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        public Guid ProductId { get; set; }

        public Guid ApplicationUserId { get; set; }
    }
    public class UpdateProductReviewRequest
    {
        [Required]
        public int Rate { get; set; }
        public string? Content { get; set; }
    }
}
