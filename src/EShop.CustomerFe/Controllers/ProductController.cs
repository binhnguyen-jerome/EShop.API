using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EShop.CustomerFe.Controllers
{
    public class ProductController(
        ILogger<ProductController> logger,
        IProductClientService productService,
        IProductReviewClientService productReviewService)
        : Controller
    {
        private readonly ILogger<ProductController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> Detail(Guid productId)
        {
            var product = await productService.GetProductByIdAsync(productId);
            var reviews = await productReviewService.GetProductReviewsAsync(productId);
            var productDetailViewModel = ProductDetailVm.Create(product, reviews);
            return View(productDetailViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReview(ProductDetailVm model)
        {
            await productReviewService.CreateProductReviewAsync(model.NewReview);
            return RedirectToAction("Detail", new { productId = model.NewReview.ProductId });
        }
    }
}
