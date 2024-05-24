using EShop.Core.Services.Interfaces;
using EShop.ViewModels.ProductViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/products/")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            List<ProductResponse> products = await productService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetByCategory(Guid? categoryId)
        {
            List<ProductResponse> products = await productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid? id)
        {
            ProductResponse? product = await productService.GetProductByIdAsync(id);
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest productRequest)
        {
            ProductResponse productResponse = await productService.CreateProductAsync(productRequest);
            return CreatedAtAction(nameof(CreateProduct), new { id = productResponse.Id }, productResponse);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid? id, [FromBody] UpdateProductRequest productRequest)
        {
            ProductResponse productResponse = await productService.UpdateProductAsync(id, productRequest);
            return Ok(productResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid? id)
        {
            if (await productService.DeleteProductAsync(id))
            {
                return Ok();
            }
            return NoContent();
        }
    }
}
