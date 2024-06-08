using EShop.ViewModels.Dtos.Category;

namespace EShop.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategoriesAsync();
        Task<CategoryResponse> GetCategoryByIdAsync(Guid id);
        Task<CategoryResponse> CreateCategoryAsync(CategoryRequest category);
        Task<CategoryResponse> UpdateCategoryAsync(Guid id, CategoryRequest category);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
