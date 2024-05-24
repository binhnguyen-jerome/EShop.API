using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.Review;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/productReviews/")]
    [ApiController]
    public class ProductReviewController : Controller
    {
        private readonly IProductReviewService productReviewService;

        public ProductReviewController(IProductReviewService productReviewService)
        {
            this.productReviewService = productReviewService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductReview(Guid? productId)
        {
            List<ProductReviewResponse> productReviewResponses = await productReviewService.GetProductReviewsByProductIdAsync(productId);
            return Ok(productReviewResponses);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductReview(ProductReviewRequest productReviewRequest)
        {
            ProductReviewResponse productReview = await productReviewService.CreateProductReviewAsync(productReviewRequest);
            return Ok(productReview);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductReview(Guid? id, UpdateProductReviewRequest updateProductReviewRequest)
        {
            ProductReviewResponse productReview = await productReviewService.UpdateProductReviewAsync(id, updateProductReviewRequest);
            return Ok(productReview);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductReview(Guid? id)
        {
            bool result = await productReviewService.DeleteProductReviewAsync(id);
            return Ok(result);
        }
    }
}
