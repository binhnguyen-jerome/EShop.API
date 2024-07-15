using EShop.Application.Services.Interfaces;
using EShop.ViewModels.Dtos.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/productReviews/")]
    [ApiController]
    [Authorize]
    public class RatingController(IProductReviewService productReviewService) : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProductReview(int productId)
        {
            var productReviewResponses = await productReviewService.GetProductReviewsAsync(productId);
            return Ok(productReviewResponses);
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateProductReview([FromBody] RatingRequest ratingRequest)
        {
            var productReview = await productReviewService.CreateProductReviewAsync(ratingRequest);
            return Ok(productReview);
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProductReview([FromRoute] int id, [FromBody] UpdateRatingRequest updateProductReviewRequest)
        {
            var productReview = await productReviewService.UpdateProductReviewAsync(id, updateProductReviewRequest);
            return Ok(productReview);
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProductReview([FromRoute] int id)
        {
            var result = await productReviewService.DeleteProductReviewAsync(id);
            return Ok(result);
        }
    }
}
