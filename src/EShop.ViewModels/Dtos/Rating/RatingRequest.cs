using System.ComponentModel.DataAnnotations;

namespace EShop.ViewModels.Dtos.Review
{
    public class RatingRequest
    {
        [Required]
        public int Rate { get; set; }
        public string? Content { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        public int ProductId { get; set; }

        public int ApplicationUserId { get; set; }
    }
    public class UpdateRatingRequest
    {
        [Required]
        public int Rate { get; set; }
        public string? Content { get; set; }
    }
}
