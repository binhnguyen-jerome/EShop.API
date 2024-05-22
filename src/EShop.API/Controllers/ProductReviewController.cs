using EShop.Core.Services.Interfaces;
using EShop.ViewModels.ProductReviewViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/productReview")]
    [ApiController]
    public class ProductReviewController : Controller
    {
        private readonly IProductReviewService productReviewService;

        public ProductReviewController(IProductReviewService productReviewService)
        {
            this.productReviewService = productReviewService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductReviewAsync(Guid? productId)
        {
            List<ProductReviewResponse> productReviewResponses = await productReviewService.GetProductReviewsByProductIdAsync(productId);
            return Ok(productReviewResponses);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateProductReviewAsync(ProductReviewRequest productReviewRequest)
        {
            ProductReviewResponse productReview = await productReviewService.CreateProductReviewAsync(productReviewRequest);
            return Ok(productReview);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProductReviewAsync(Guid? id, UpdateProductReviewRequest updateProductReviewRequest)
        {
            ProductReviewResponse productReview = await productReviewService.UpdateProductReviewAsync(id, updateProductReviewRequest);
            return Ok(productReview);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProductReviewAsync(Guid? id)
        {
            bool result = await productReviewService.DeleteProductReviewAsync(id);
            return Ok(result);
        }
    }
}
