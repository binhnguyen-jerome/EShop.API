using EShop.ViewModels.ProductReviewViewModel;

namespace EShop.ViewModels.ProductViewModel
{
    public class ProductDetailViewModel
    {
        public ProductResponse Product { get; set; }
        public List<ProductReviewUserResponse> Reviews { get; set; }

        public ProductReviewRequest NewReview { get; set; }
    }
}
