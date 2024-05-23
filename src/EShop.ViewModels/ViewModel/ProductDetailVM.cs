using EShop.ViewModels.Dtos.Review;
using EShop.ViewModels.ProductViewModel;

namespace EShop.ViewModels.ViewModel
{
    public class ProductDetailVM
    {
        public ProductResponse Product { get; set; }
        public List<ProductReviewUserResponse> Reviews { get; set; }

        public ProductReviewRequest NewReview { get; set; }
    }
}
