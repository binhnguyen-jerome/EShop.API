﻿using EShop.Core.IServices;
using EShop.ViewModels.ProductReviewViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/productReview")]
    [ApiController]
    public class ProductReviewController : Controller
    {
        private readonly IProductReviewService productReviewService;

        public ProductReviewController(IProductReviewService productReviewService)
        {
            this.productReviewService = productReviewService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductReviewAsync(Guid? id)
        {
            List<ProductReviewResponse> productReviewResponses = await productReviewService.GetProductReviewsByProductIdAsync(id);
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