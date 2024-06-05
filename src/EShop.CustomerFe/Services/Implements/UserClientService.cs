using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.User;
using Newtonsoft.Json;

namespace EShop.CustomerFe.Services.Implements
{
    public class UserClientService : IUserClientService
    {
        private readonly HttpClient _httpClient;

        public UserClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserReponse?> GetUserById(Guid userId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/users/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserReponse>(content);
            }
            return null;
        }
    }
}
