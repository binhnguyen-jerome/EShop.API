using EShop.ViewModels.CategoryViewModel;

namespace EShop.CustomerFe.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategoriesAsync();
    }
}
