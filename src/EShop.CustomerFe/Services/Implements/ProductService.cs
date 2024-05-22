using EShop.CustomerFe.Services.Interface;
using EShop.ViewModels.ProductViewModel;
using Newtonsoft.Json;

namespace EShop.CustomerFe.Services
{
    public class ProductService : IProductService
    {
        Uri uri = new Uri("https://localhost:7045/api");
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = uri;
        }
        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/v1/product/all");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductResponse>>(content);
        }

        public async Task<ProductResponse> GetProductByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/v1/product/get/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProductResponse>(content);
        }

        public async Task<List<ProductResponse>> GetProductByCategoryIdAsync(Guid categoryId)
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/v1/product/all/category?categoryId={categoryId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductResponse>>(content);
        }
    }
}
