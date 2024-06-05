using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.Order;
using Newtonsoft.Json;
using System.Text;

namespace EShop.CustomerFe.Services.Implements
{
    public class OrderClientService : IOrderClientService
    {
        private readonly HttpClient _httpClient;

        public OrderClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<OrderResponse?> CreateOrderAsync(OrderRequest orderRequest)
        {
            var json = JsonConvert.SerializeObject(orderRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/v1/orders", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OrderResponse>(responseContent);
            }
            return null;
        }
    }
}
