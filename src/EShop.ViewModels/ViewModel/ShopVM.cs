using EShop.ViewModels.Dtos.Category;
using EShop.ViewModels.Dtos.Product;

namespace EShop.ViewModels.ViewModel
{
    public class ShopVM
    {
        public PagedResult<ProductResponse> Products { get; set; }
        public List<CategoryResponse> Categories { get; set; }

        public CategoryResponse SelectedCategory { get; set; }

        public ProductQuery ProductQuery { get; set; }

        public static ShopVM Create(PagedResult<ProductResponse> products, List<CategoryResponse> categories, CategoryResponse selectedCategory, ProductQuery productQuery)
        {
            return new ShopVM
            {
                Products = products,
                Categories = categories,
                SelectedCategory = selectedCategory,
                ProductQuery = productQuery
            };
        }
    }
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalItems / PageSize);
            }
        }
    }
}
