using EShop.CustomerFe.Services.Interface;
using EShop.ViewModels.Dtos.Product;
using EShop.ViewModels.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EShop.CustomerFe.Controllers
{
    public class ShopController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IProductClientService productService;
        private readonly ICategoryClientService categoryService;

        public ShopController(ILogger<HomeController> logger, IProductClientService productService, ICategoryClientService categoryService)
        {
            _logger = logger;
            this.productService = productService;
            this.categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(ProductQuery productQuery)
        {
            var products = await productService.GetFilterProductsAsync(productQuery);
            var categories = await categoryService.GetAllCategoriesAsync();

            var shopViewModel = new ShopVM
            {
                Products = products,
                SelectedCategory = categories.FirstOrDefault(c => c.Id == productQuery.CategoryId),
                Categories = categories,
                ProductQuery = productQuery
            };
            return View(shopViewModel);
        }
    }
}
