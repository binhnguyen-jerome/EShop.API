using EShop.ViewModels.Dtos.Category;
using EShop.ViewModels.ProductViewModel;

namespace EShop.ViewModels.ViewModel
{
    public class HomeVM
    {
        public List<CategoryResponse> Categories { get; set; }
        public List<ProductResponse> Products { get; set; }
    }
}
