using EShop.CustomerFe.Services.Interface;
using EShop.ViewModels.CategoryViewModel;
using Newtonsoft.Json;

namespace EShop.CustomerFe.Services.Implement
{
    public class CategoryService : ICategoryService
    {
        Uri uri = new Uri("https://localhost:7045/api");
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = uri;
        }
        public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/v1/category/all");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CategoryResponse>>(content);
        }
    }
}
