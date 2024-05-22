using EShop.ViewModels.CategoryViewModel;

namespace EShop.Core.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategoriesAsync();
        Task<CategoryResponse?> GetCategoryByIdAsync(Guid? id);
        Task<CategoryResponse> CreateCategoryAsync(CategoryRequest? category);
        Task<CategoryResponse> UpdateCategoryAsync(Guid? id, CategoryRequest? category);
        Task<CategoryResponse> DeleteCategoryAsync(Guid? id);
    }
}
