using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.Cart;
using Newtonsoft.Json;
using System.Text;

namespace EShop.CustomerFe.Services.Implements
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<CartResponse> AddToCartAsync(CartRequest cartRequest)
        {
            var json = JsonConvert.SerializeObject(cartRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/v1/carts", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CartResponse>(responseContent);
            }
            return null;
        }

        public async Task<List<CartResponse>> GetCartByUserIdAsync(Guid userId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/carts/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<CartResponse>>(content);
            }
            return [];

        }

        public async Task<bool> UpdateCartAsync(CartRequest cartRequest)
        {
            var json = JsonConvert.SerializeObject(cartRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/v1/carts", content);
            if (response.IsSuccessStatusCode)
            {
                var reponseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(reponseContent);
            }
            return false;
        }

        public async Task<bool> RemoveFromCartAsync(Guid cartId)
        {
            var response = await _httpClient.DeleteAsync($"/api/v1/carts/{cartId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(content);
            }
            return false;
        }
    }
}
