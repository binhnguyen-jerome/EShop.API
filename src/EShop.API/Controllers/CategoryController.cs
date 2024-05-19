using EShop.Core.IServices;
using EShop.ViewModels.CategoryViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        //[Authorize(Roles = "Admin")]

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            List<CategoryResponse> categories = await categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CategoryRequest categoryRequest)
        {
            CategoryResponse category = await categoryService.CreateCategoryAsync(categoryRequest);
            return CreatedAtAction(nameof(Create), new { id = category.Id }, category);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid? id)
        {
            CategoryResponse? categoryResponse = await categoryService.GetCategoryByIdAsync(id);
            return Ok(categoryResponse);
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid? id, [FromBody] CategoryRequest categoryRequest)
        {
            CategoryResponse categoryResponse = await categoryService.UpdateCategoryAsync(id, categoryRequest);
            return Ok(categoryResponse);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid? id)
        {
            CategoryResponse categoryResponse = await categoryService.DeleteCategoryAsync(id);
            return Ok(categoryResponse);
        }
    }
}
