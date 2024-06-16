using EShop.Application.Services.Interfaces;
using EShop.ViewModels.Dtos.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/categories/")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest categoryRequest)
        {
            var category = await categoryService.CreateCategoryAsync(categoryRequest);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategory([FromRoute] Guid id)
        {
            var categoryResponse = await categoryService.GetCategoryByIdAsync(id);
            return Ok(categoryResponse);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] CategoryRequest categoryRequest)
        {
            var categoryResponse = await categoryService.UpdateCategoryAsync(id, categoryRequest);
            return Ok(categoryResponse);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await categoryService.DeleteCategoryAsync(id);
            return Ok(result);
        }
    }
}
