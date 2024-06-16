using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.Review;
using Newtonsoft.Json;
using System.Text;

namespace EShop.CustomerFe.Services.Implements
{
    public class ProductReviewClientService(HttpClient httpClient) : IProductReviewClientService
    {
        public async Task<List<ProductReviewUserResponse>?> GetProductReviewsAsync(Guid productId)
        {
            var response = await httpClient.GetAsync($"/api/v1/productReviews?productId={productId}");
            if (!response.IsSuccessStatusCode) return [];
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductReviewUserResponse>>(content);
        }
        public async Task<ProductReviewResponse?> CreateProductReviewAsync(ProductReviewRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/v1/productReviews", content);
            if (!response.IsSuccessStatusCode) return null;
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProductReviewResponse>(responseContent);
        }
    }
}
