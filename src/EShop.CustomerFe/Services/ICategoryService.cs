using EShop.ViewModels.CategoryViewModel;

namespace EShop.CustomerFe.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategoriesAsync();
    }
}
