using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.Product;
using EShop.ViewModels.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EShop.CustomerFe.Controllers
{
    public class ShopController(
        ILogger<HomeController> logger,
        IProductClientService productService,
        ICategoryClientService categoryService)
        : Controller
    {

        private readonly ILogger<HomeController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> Index(ProductQuery productQuery)
        {
            var products = await productService.GetFilterProductsAsync(productQuery);
            var categories = await categoryService.GetAllCategoriesAsync();
            var selectedCategory = categories.FirstOrDefault(c => c.Id == productQuery.CategoryId);

            var shopViewModel = ShopVM.Create(products, categories, selectedCategory, productQuery);

            return View(shopViewModel);
        }
    }
}
