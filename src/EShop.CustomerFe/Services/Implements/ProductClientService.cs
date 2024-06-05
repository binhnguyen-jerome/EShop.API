using EShop.CustomerFe.Services.Interface;
using EShop.ViewModels.Dtos.Product;
using Newtonsoft.Json;

namespace EShop.CustomerFe.Services
{
    public class ProductClientService : IProductClientService
    {

        private readonly HttpClient _httpClient;

        public ProductClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ProductResponse>?> GetAllProductsAsync()
        {

            var response = await _httpClient.GetAsync($"/api/v1/products?");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductResponse>>(content);
            }
            return [];
        }

        public async Task<ProductResponse?> GetProductByIdAsync(Guid productId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/products/{productId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductResponse>(content);
            }
            return null;
        }
        public async Task<List<ProductResponse>?> GetFilterProducts(ProductQuery query)
        {
            var queryString = BuildQueryString(query);
            var url = $"/api/v1/products{queryString}";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductResponse>>(content);
            }
            return [];
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

            if (query.MinPrice != null)
            {
                queryString += $"minPrice={query.MinPrice}&";
            }

            if (query.PageNumber > 0)
            {
                queryString += $"pageNumber={query.PageNumber}&";
            }

            queryString = queryString.TrimEnd('&');

            return queryString;
        }
    }
}
