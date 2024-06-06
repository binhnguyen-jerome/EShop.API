using EShop.CustomerFe.Services.Interface;
using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.Product;
using EShop.ViewModels.ViewModel;
using Newtonsoft.Json;
using System.Reflection;

namespace EShop.CustomerFe.Services
{
    public class ProductClientService : IProductClientService
    {

        private readonly HttpClient _httpClient;
        private readonly ICacheClientService _cacheService;
        private readonly string _cacheKey = "AllProducts";

        public ProductClientService(HttpClient httpClient, ICacheClientService cacheService)
        {
            _httpClient = httpClient;
            _cacheService = cacheService;
        }
        public async Task<List<ProductResponse>?> GetAllProductsAsync()
        {
            var products = _cacheService.Get<List<ProductResponse>>(_cacheKey);

            if (products == null)
            {
                var url = "/api/v1/products";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<ProductResponse>>(content);

                    _cacheService.Set(_cacheKey, products, TimeSpan.FromMinutes(60));
                }
            }

            return products;
        }
        public async Task<PagedResult<ProductResponse>> GetFilterProductsAsync(ProductQuery query)
        {
            var allProducts = await GetAllProductsAsync();

            if (query.CategoryId.HasValue)
            {
                allProducts = allProducts.Where(p => p.CategoryId == query.CategoryId.Value).ToList();
            }

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var propertyInfo = typeof(ProductResponse).GetProperty(query.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    allProducts = query.SortDescending
                        ? allProducts.OrderByDescending(p => propertyInfo.GetValue(p, null)).ToList()
                        : allProducts.OrderBy(p => propertyInfo.GetValue(p, null)).ToList();
                }
            }

            int skip = (query.PageNumber - 1) * query.PageSize;
            var pagedProducts = allProducts.Skip(skip).Take(query.PageSize).ToList();
            return new PagedResult<ProductResponse>
            {
                Items = pagedProducts,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalItems = allProducts.Count
            };
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
    }
}
