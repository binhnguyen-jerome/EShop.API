using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.Review;
using Newtonsoft.Json;
using System.Text;

namespace EShop.CustomerFe.Services.Implements
{
    public class RatingClientService(HttpClient httpClient) : IRatingClientService
    {
        public async Task<List<RatingUserResponse>?> GetProductReviewsAsync(int productId)
        {
            var response = await httpClient.GetAsync($"/api/v1/productReviews?productId={productId}");
            if (!response.IsSuccessStatusCode) return [];
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<RatingUserResponse>>(content);
        }
        public async Task<RatingResponse?> CreateProductReviewAsync(RatingRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/v1/productReviews", content);
            if (!response.IsSuccessStatusCode) return null;
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RatingResponse>(responseContent);
        }
    }
}
