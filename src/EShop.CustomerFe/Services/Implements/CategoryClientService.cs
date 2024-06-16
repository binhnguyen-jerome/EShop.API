using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.Category;
using Newtonsoft.Json;

namespace EShop.CustomerFe.Services.Implements
{
    public class CategoryClientService(HttpClient httpClient) : ICategoryClientService
    {
        public async Task<List<CategoryResponse>?> GetAllCategoriesAsync()
        {
            var response = await httpClient.GetAsync("/api/v1/categories");
            if (!response.IsSuccessStatusCode) return [];
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CategoryResponse>>(content);
        }
    }
}
