using System.Reflection;
using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.Product;
using EShop.ViewModels.ViewModel;
using Newtonsoft.Json;

namespace EShop.CustomerFe.Services.Implements
{
    public class ProductClientService(HttpClient httpClient, ICacheClientService cacheService) : IProductClientService
    {
        private readonly string _cacheKey = "AllProducts";

        public async Task<List<ProductResponse>?> GetAllProductsAsync()
        {
            var products = cacheService.Get<List<ProductResponse>>(_cacheKey);
            
            var response = await httpClient.GetAsync("/api/v1/products");

            if (!response.IsSuccessStatusCode) return [];
            var content = await response.Content.ReadAsStringAsync();
            products = JsonConvert.DeserializeObject<List<ProductResponse>>(content);

            cacheService.Set(_cacheKey, products, TimeSpan.FromMinutes(60));

            return products;
        }
        public async Task<PagedResult<ProductResponse>> GetFilterProductsAsync(ProductQuery query)
        {
            var allProducts = await GetAllProductsAsync();

            if (query.CategoryId.HasValue)
            {
                allProducts = allProducts?.Where(p => p.CategoryId == query.CategoryId.Value).ToList();
            }

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var propertyInfo = typeof(ProductResponse).GetProperty(query.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    if (allProducts != null)
                        allProducts = query.SortDescending
                            ? allProducts.OrderByDescending(p => propertyInfo.GetValue(p, null)).ToList()
                            : allProducts.OrderBy(p => propertyInfo.GetValue(p, null)).ToList();
                }
            }

            var skip = (query.PageNumber - 1) * query.PageSize;
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
            var response = await httpClient.GetAsync($"/api/v1/products/{productId}");
            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProductResponse>(content);
        }
    }
}
