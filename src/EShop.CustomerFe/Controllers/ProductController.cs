using EShop.CustomerFe.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShop.CustomerFe.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            this.productService = productService;
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            var product = await productService.GetProductByIdAsync(id);
            return View(product);
        }
    }
}
