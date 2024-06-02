using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.Category;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            List<CategoryResponse> categories = await categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest categoryRequest)
        {
            CategoryResponse category = await categoryService.CreateCategoryAsync(categoryRequest);
            return CreatedAtAction(nameof(CreateCategory), new { id = category.Id }, category);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] Guid? id)
        {
            CategoryResponse? categoryResponse = await categoryService.GetCategoryByIdAsync(id.Value);
            return Ok(categoryResponse);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid? id, [FromBody] CategoryRequest categoryRequest)
        {
            CategoryResponse categoryResponse = await categoryService.UpdateCategoryAsync(id.Value, categoryRequest);
            return Ok(categoryResponse);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid? id)
        {
            CategoryResponse categoryResponse = await categoryService.DeleteCategoryAsync(id.Value);
            return Ok(categoryResponse);
        }
    }
}
