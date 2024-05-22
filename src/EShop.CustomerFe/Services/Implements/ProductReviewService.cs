using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.ProductReviewViewModel;
using Newtonsoft.Json;
using System.Text;

namespace EShop.CustomerFe.Services.Implements
{
    public class ProductReviewService : IProductReviewService
    {
        Uri uri = new Uri("https://localhost:7045/api");
        private readonly HttpClient _httpClient;

        public ProductReviewService(HttpClient httpClient)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = uri;
        }
        public async Task<List<ProductReviewUserResponse>> GetProductReviewsAsync(Guid productId)
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/v1/productReview?productId={productId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductReviewUserResponse>>(content);
        }
        public async Task<ProductReviewResponse> CreateProductReviewAsync(ProductReviewRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/v1/productReview/create", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProductReviewResponse>(responseContent);
        }
    }
}
