using EShop.ViewModels.Dtos.Cart;
using EShop.ViewModels.Dtos.Product;
using EShop.ViewModels.Dtos.Review;

namespace EShop.ViewModels.ViewModel
{
    public class ProductDetailVM
    {
        public ProductResponse Product { get; set; } = new();
        public List<ProductReviewUserResponse> Reviews { get; set; } = new();

        public ProductReviewRequest NewReview { get; set; } = new();

        public CartRequest CartRequest { get; set; } = new();

        public int AverageRating { get; set; }

        public static ProductDetailVM Create(ProductResponse product, List<ProductReviewUserResponse> reviews)
        {
            return new ProductDetailVM
            {
                Product = product,
                Reviews = reviews,
                AverageRating = reviews.Any() ? (int)Math.Floor(reviews.Average(r => r.Rate)) : 0
            };
        }
    }
}
