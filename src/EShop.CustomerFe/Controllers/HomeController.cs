using EShop.CustomerFe.Models;
using EShop.CustomerFe.Services;
using EShop.ViewModels.HomeViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EShop.CustomerFe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            this.productService = productService;
            this.categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllProductsAsync();
            var categories = await categoryService.GetAllCategoriesAsync();
            var viewModel = new HomeViewModel
            {
                Products = products,
                Categories = categories
            };
            ViewBag.Categories = categories;
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
