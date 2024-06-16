using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.Cart;
using Newtonsoft.Json;
using System.Text;

namespace EShop.CustomerFe.Services.Implements
{
    public class CartClientService(HttpClient httpClient) : ICartClientService
    {
        public async Task<CartResponse?> AddToCartAsync(CartRequest cartRequest)
        {
            var json = JsonConvert.SerializeObject(cartRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/v1/carts", content);
            if (!response.IsSuccessStatusCode) return null;
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CartResponse>(responseContent);
        }

        public async Task<List<CartResponse>?> GetCartByUserIdAsync(Guid userId)
        {
            var response = await httpClient.GetAsync($"/api/v1/carts/{userId}");
            if (!response.IsSuccessStatusCode) return [];
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CartResponse>>(content);

        }

        public async Task<bool> UpdateCartAsync(CartRequest cartRequest)
        {
            var json = JsonConvert.SerializeObject(cartRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"/api/v1/carts", content);
            if (!response.IsSuccessStatusCode) return false;
            var reponseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(reponseContent);
        }

        public async Task<bool> RemoveFromCartAsync(Guid cartId)
        {
            var response = await httpClient.DeleteAsync($"/api/v1/carts/{cartId}");
            if (!response.IsSuccessStatusCode) return false;
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(content);
        }
    }
}
