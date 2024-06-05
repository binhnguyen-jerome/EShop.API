using EShop.CustomerFe.Services.Interface;
using EShop.ViewModels.Dtos.Product;
using EShop.ViewModels.ViewModel;
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
        [HttpGet]
        public async Task<IActionResult> Index(Guid categoryId)
        {
            var query = new ProductQuery { CategoryId = categoryId };
            var products = await productService.GetFilterProducts(query);
            var category = products.FirstOrDefault()?.CategoryName;
            var categories = await categoryService.GetAllCategoriesAsync();
            var shopViewModel = new ShopVM
            {
                Products = products,
                CategoryName = category,
                Categories = categories
            };
            return View(shopViewModel);
        }
    }
}
