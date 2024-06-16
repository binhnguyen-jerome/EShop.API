using EShop.Application.Services.Interfaces;
using EShop.ViewModels.Dtos.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/productReviews/")]
    [ApiController]
    [Authorize]
    public class ProductReviewController(IProductReviewService productReviewService) : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProductReview(Guid productId)
        {
            var productReviewResponses = await productReviewService.GetProductReviewsAsync(productId);
            return Ok(productReviewResponses);
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateProductReview([FromBody] ProductReviewRequest productReviewRequest)
        {
            var productReview = await productReviewService.CreateProductReviewAsync(productReviewRequest);
            return Ok(productReview);
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProductReview([FromRoute] Guid id, [FromBody] UpdateProductReviewRequest updateProductReviewRequest)
        {
            var productReview = await productReviewService.UpdateProductReviewAsync(id, updateProductReviewRequest);
            return Ok(productReview);
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProductReview([FromRoute] Guid id)
        {
            var result = await productReviewService.DeleteProductReviewAsync(id);
            return Ok(result);
        }
    }
}
