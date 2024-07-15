using EShop.ViewModels.Dtos.Cart;
using EShop.ViewModels.Dtos.Product;
using EShop.ViewModels.Dtos.Review;

namespace EShop.ViewModels.ViewModel
{
    public class ProductDetailVm
    {
        public ProductResponse Product { get; set; } = new();
        public List<RatingUserResponse> Reviews { get; set; } = [];

        public RatingRequest NewReview { get; set; } = new();

        public CartRequest CartRequest { get; set; } = new();

        public int AverageRating { get; set; }

        public static ProductDetailVm Create(ProductResponse product, List<RatingUserResponse> reviews)
        {
            return new ProductDetailVm
            {
                Product = product,
                Reviews = reviews,
                AverageRating = reviews.Count != 0 ? (int)Math.Floor(reviews.Average(r => r.Rate)) : 0
            };
        }
    }
}
