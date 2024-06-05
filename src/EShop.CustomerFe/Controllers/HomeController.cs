using EShop.CustomerFe.Models;
using EShop.CustomerFe.Services.Interface;
using EShop.ViewModels.ViewModel;
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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllProductsAsync();
            var categories = await categoryService.GetAllCategoriesAsync();

            HomeVM viewModel = new()
            {
                Products = products,
                Categories = categories
            };
            ViewBag.Categories = categories;
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
