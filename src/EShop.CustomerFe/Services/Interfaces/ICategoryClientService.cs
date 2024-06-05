using EShop.ViewModels.Dtos.Category;

namespace EShop.CustomerFe.Services.Interface
{
    public interface ICategoryClientService
    {
        Task<List<CategoryResponse>?> GetAllCategoriesAsync();
    }
}
