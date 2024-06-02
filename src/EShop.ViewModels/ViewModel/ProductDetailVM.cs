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
    }
}
