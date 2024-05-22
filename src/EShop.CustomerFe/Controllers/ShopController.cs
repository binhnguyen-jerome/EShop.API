using EShop.CustomerFe.Services.Interface;
using EShop.ViewModels.ShopViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EShop.CustomerFe.Controllers
{
    public class ShopController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ShopController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            this.productService = productService;
            this.categoryService = categoryService;
        }
        public async Task<IActionResult> Page(Guid categoryId)
        {
            var products = await productService.GetProductByCategoryIdAsync(categoryId);
            var category = products.FirstOrDefault()?.CategoryName;
            var categories = await categoryService.GetAllCategoriesAsync();
            var shopViewModel = new ShopViewModel
            {
                Products = products,
                CategoryName = category,
                Categories = categories
            };
            return View(shopViewModel);
        }
    }
}
