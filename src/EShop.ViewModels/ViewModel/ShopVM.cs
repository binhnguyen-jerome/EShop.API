using EShop.ViewModels.Dtos.Category;
using EShop.ViewModels.ProductViewModel;

namespace EShop.ViewModels.ViewModel
{
    public class ShopVM
    {
        public List<ProductResponse> Products { get; set; }
        public List<CategoryResponse> Categories { get; set; }
        public string CategoryName { get; set; }
    }
}
