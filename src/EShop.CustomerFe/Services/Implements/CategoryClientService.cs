using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.Category;
using Newtonsoft.Json;

namespace EShop.CustomerFe.Services.Implements
{
    public class CategoryClientService : ICategoryClientService
    {
        private readonly HttpClient _httpClient;

        public CategoryClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<CategoryResponse>?> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("/api/v1/categories");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<CategoryResponse>>(content);
            }
            return [];
        }
    }
}
