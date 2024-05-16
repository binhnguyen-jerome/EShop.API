using EShop.ViewModels.CategoryViewModel;

namespace EShop.Core.IServices
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategories();
        Task<CategoryResponse?> GetCategoryById(Guid? id);
        Task<CategoryResponse> AddCategory(CategoryRequest? category);
        Task<CategoryResponse> UpdateCategory(Guid? id, CategoryRequest? category);
        Task<CategoryResponse> DeleteCategory(Guid? id);
    }
}
