using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.Category;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/categories/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        //[Authorize(Roles = "Admin")]

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            List<CategoryResponse> categories = await categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest categoryRequest)
        {
            CategoryResponse category = await categoryService.CreateCategoryAsync(categoryRequest);
            return CreatedAtAction(nameof(CreateCategory), new { id = category.Id }, category);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] Guid? id)
        {
            CategoryResponse? categoryResponse = await categoryService.GetCategoryByIdAsync(id);
            return Ok(categoryResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid? id, [FromBody] CategoryRequest categoryRequest)
        {
            CategoryResponse categoryResponse = await categoryService.UpdateCategoryAsync(id, categoryRequest);
            return Ok(categoryResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid? id)
        {
            CategoryResponse categoryResponse = await categoryService.DeleteCategoryAsync(id);
            return Ok(categoryResponse);
        }
    }
}
