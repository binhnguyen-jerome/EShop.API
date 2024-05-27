using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.Review;
using Newtonsoft.Json;
using System.Text;

namespace EShop.CustomerFe.Services.Implements
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly HttpClient _httpClient;

        public ProductReviewService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ProductReviewUserResponse>> GetProductReviewsAsync(Guid productId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/productReviews?productId={productId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductReviewUserResponse>>(content);
        }
        public async Task<ProductReviewResponse> CreateProductReviewAsync(ProductReviewRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/v1/productReviews", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProductReviewResponse>(responseContent);
        }
    }
}
