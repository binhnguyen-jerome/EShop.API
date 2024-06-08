using EShop.Application.Services.Interfaces;
using EShop.ViewModels.Dtos.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/products/")]
    [ApiController]
    public class ProductController(IProductService productService) : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await productService.GetProductsAsync();
            return Ok(products);
        }
        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {
            var product = await productService.GetProductByIdAsync(id);
            return Ok(product);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest productRequest)
        {
            var result = await productService.CreateProductAsync(productRequest);
            return CreatedAtAction(nameof(CreateProduct), result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest productRequest)
        {
            var productResponse = await productService.UpdateProductAsync(id, productRequest);
            return Ok(productResponse);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var result = await productService.DeleteProductAsync(id);
            return Ok(result);
        }
    }
}
