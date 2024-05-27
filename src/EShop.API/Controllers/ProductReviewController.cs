using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/productReviews/")]
    [ApiController]
    [Authorize]
    public class ProductReviewController : Controller
    {
        private readonly IProductReviewService productReviewService;

        public ProductReviewController(IProductReviewService productReviewService)
        {
            this.productReviewService = productReviewService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProductReview([FromQuery] ProductReviewQuery query)
        {
            List<ProductReviewResponse> productReviewResponses = await productReviewService.GetProductReviewsAsync(query);
            return Ok(productReviewResponses);
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateProductReview([FromBody] ProductReviewRequest productReviewRequest)
        {
            ProductReviewResponse productReview = await productReviewService.CreateProductReviewAsync(productReviewRequest);
            return Ok(productReview);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductReview([FromRoute] Guid id, [FromBody] UpdateProductReviewRequest updateProductReviewRequest)
        {
            ProductReviewResponse productReview = await productReviewService.UpdateProductReviewAsync(id, updateProductReviewRequest);
            return Ok(productReview);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductReview([FromRoute] Guid id)
        {
            bool result = await productReviewService.DeleteProductReviewAsync(id);
            return Ok(result);
        }
    }
}
