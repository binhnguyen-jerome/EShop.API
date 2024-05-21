using EShop.ViewModels.CategoryViewModel;
using EShop.ViewModels.ProductViewModel;

namespace EShop.ViewModels.ShopViewModel
{
    public class ShopViewModel
    {
        public List<ProductResponse> Products { get; set; }
        public List<CategoryResponse> Categories { get; set; }
        public string CategoryName { get; set; }
    }
}
