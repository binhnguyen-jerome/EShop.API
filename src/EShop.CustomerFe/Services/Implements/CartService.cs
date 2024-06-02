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
        public async Task<CartResponse> AddToCartAsync(CartRequest? cartRequest)
        {
            var json = JsonConvert.SerializeObject(cartRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/v1/carts", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CartResponse>(responseContent);
        }

        public async Task<List<CartResponse>> GetCartByUserIdAsync(Guid userId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/carts/{userId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CartResponse>>(content);
        }

        public Task<bool> MinusAsync(Guid cartId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PlusAsync(Guid cartId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromCartAsync(Guid cartId)
        {
            throw new NotImplementedException();
        }
    }
}
