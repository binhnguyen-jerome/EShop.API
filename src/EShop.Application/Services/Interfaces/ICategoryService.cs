using EShop.ViewModels.Dtos.Category;

namespace EShop.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategoriesAsync();
        Task<CategoryResponse> GetCategoryByIdAsync(int id);
        Task<CategoryResponse> CreateCategoryAsync(CategoryRequest category);
        Task<CategoryResponse> UpdateCategoryAsync(int id, CategoryRequest category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
