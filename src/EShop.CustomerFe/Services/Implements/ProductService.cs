using EShop.CustomerFe.Services.Interface;
using EShop.ViewModels.Dtos.Product;
using EShop.ViewModels.ProductViewModel;
using Newtonsoft.Json;

namespace EShop.CustomerFe.Services
{
    public class ProductService : IProductService
    {

        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {

            var response = await _httpClient.GetAsync($"/api/v1/products?");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductResponse>>(content);
        }

        public async Task<ProductResponse> GetProductByIdAsync(Guid productId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/products/{productId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProductResponse>(content);
        }
        public async Task<List<ProductResponse>> GetFilterProducts(ProductQuery query)
        {
            var queryString = BuildQueryString(query);
            var url = $"/api/v1/products{queryString}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductResponse>>(content);
        }

        private string BuildQueryString(ProductQuery query)
        {
            var queryString = "?";

            if (query.CategoryId != null)
            {
                queryString += $"categoryId={query.CategoryId}&";
            }

            if (query.MaxPrice != null)
            {
                queryString += $"maxPrice={query.MaxPrice}&";
            }

            queryString = queryString.TrimEnd('&');

            return queryString;
        }
    }
}
