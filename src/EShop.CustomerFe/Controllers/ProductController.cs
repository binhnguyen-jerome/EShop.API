using EShop.CustomerFe.Services.Interface;
using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EShop.CustomerFe.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductClientService productService;
        private readonly IProductReviewClientService productReviewService;

        public ProductController(ILogger<ProductController> logger, IProductClientService productService, IProductReviewClientService productReviewService)
        {
            _logger = logger;
            this.productService = productService;
            this.productReviewService = productReviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid productId)
        {
            var product = await productService.GetProductByIdAsync(productId);
            var reviews = await productReviewService.GetProductReviewsAsync(productId);
            var productDetailViewModel = ProductDetailVM.Create(product, reviews);
            return View(productDetailViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReview(ProductDetailVM model)
        {
            await productReviewService.CreateProductReviewAsync(model.NewReview);
            return RedirectToAction("Detail", new { productId = model.NewReview.ProductId });
        }
    }
}
