using EShop.ViewModels.Dtos.Category;

namespace EShop.CustomerFe.Services.Interfaces
{
    public interface ICategoryClientService
    {
        Task<List<CategoryResponse>?> GetAllCategoriesAsync();
    }
}
