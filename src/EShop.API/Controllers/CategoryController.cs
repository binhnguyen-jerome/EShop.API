using EShop.Core.DTO.RequestDto;
using EShop.Core.DTO.ResponseDto;
using EShop.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns> Status code of the action </returns>
        /// <response code="200">Successfully get items information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            List<CategoryResponse> categories = await categoryService.GetAllCategories();
            return Ok(categories);
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="categoryRequest"></param>
        /// <returns>Status code of the action.</returns>
        /// <response code="201">Successfully created item.</response>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create(CategoryRequest categoryRequest)
        {
            CategoryResponse category = await categoryService.AddCategory(categoryRequest);
            return CreatedAtAction(nameof(Create), new { id = category.Id }, category);
        }

        /// <summary>
        /// Update category by id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryRequest"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(Guid? id, CategoryRequest categoryRequest)
        {
            CategoryResponse categoryResponse = await categoryService.UpdateCategory(id, categoryRequest);
            return Ok(categoryResponse);
        }

        /// <summary>
        /// Delete category by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            CategoryResponse categoryResponse = await categoryService.DeleteCategory(id);
            return Ok(categoryResponse);
        }
    }
}
