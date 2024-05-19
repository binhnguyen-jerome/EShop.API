using EShop.Core.IServices;
using EShop.ViewModels.ProductViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/product")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            List<ProductResponse> products = await productService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet("getbycategory")]
        public async Task<IActionResult> GetByCategory(Guid? categoryId)
        {
            List<ProductResponse> products = await productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid? id)
        {
            ProductResponse? product = await productService.GetProductByIdAsync(id);
            return Ok(product);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest productRequest)
        {
            ProductResponse productResponse = await productService.CreateProductAsync(productRequest);
            return CreatedAtAction(nameof(Create), new { id = productResponse.Id }, productResponse);
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid? id, [FromBody] UpdateProductRequest productRequest)
        {
            ProductResponse productResponse = await productService.UpdateProductAsync(id, productRequest);
            return Ok(productResponse);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid? id)
        {
            if (await productService.DeleteProductAsync(id))
            {
                return Ok();
            }
            return NoContent();
        }
    }
}
