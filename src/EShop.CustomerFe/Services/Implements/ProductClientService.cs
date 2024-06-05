using EShop.CustomerFe.Services.Interface;
using EShop.ViewModels.Dtos.Product;
using EShop.ViewModels.ViewModel;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Reflection;

namespace EShop.CustomerFe.Services
{
    public class ProductClientService : IProductClientService
    {

        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "AllProducts";

        public ProductClientService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }
        public async Task<List<ProductResponse>?> GetAllProductsAsync()
        {
            if (!_cache.TryGetValue(_cacheKey, out List<ProductResponse>? products))
            {
                var url = $"/api/v1/products";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<ProductResponse>>(content);

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(60)); // Cache for 60 minutes
                    _cache.Set(_cacheKey, products, cacheEntryOptions);
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
