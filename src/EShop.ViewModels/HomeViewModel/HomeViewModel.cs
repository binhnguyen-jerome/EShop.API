using EShop.ViewModels.CategoryViewModel;
using EShop.ViewModels.ProductViewModel;

namespace EShop.ViewModels.HomeViewModel
{
    public class HomeViewModel
    {
        public List<CategoryResponse> Categories { get; set; }
        public List<ProductResponse> Products { get; set; }
    }
}
