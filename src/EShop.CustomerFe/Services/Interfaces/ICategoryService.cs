using EShop.ViewModels.Dtos.Category;

namespace EShop.CustomerFe.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategoriesAsync();
    }
}
