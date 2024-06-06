using EShop.ViewModels.Dtos.Category;
using EShop.ViewModels.Dtos.Product;

namespace EShop.ViewModels.ViewModel
{
    public class HomeVM
    {

        public List<CategoryResponse> Categories { get; set; }
        public List<ProductResponse> Products { get; set; }
        public static HomeVM Create(List<CategoryResponse> categories, List<ProductResponse> products)
        {
            return new HomeVM
            {
                Categories = categories,
                Products = products
            };
        }
    }
}
