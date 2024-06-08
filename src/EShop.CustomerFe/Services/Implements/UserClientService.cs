using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.User;
using Newtonsoft.Json;

namespace EShop.CustomerFe.Services.Implements
{
    public class UserClientService(HttpClient httpClient) : IUserClientService
    {
        public async Task<UserReponse?> GetUserById(Guid userId)
        {
            var response = await httpClient.GetAsync($"/api/v1/users/{userId}");
            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserReponse>(content);
        }
    }
}
