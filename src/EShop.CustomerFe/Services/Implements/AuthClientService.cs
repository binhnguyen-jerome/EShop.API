using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.User;
using Newtonsoft.Json;

namespace EShop.CustomerFe.Services.Implements
{
    public class AuthClientService(HttpClient httpClient) : IAuthClientService
    {
        public async Task<LoginResponse?> AuthenticateAsync(LoginRequest loginRequest)
        {
            var response = await httpClient.PostAsJsonAsync("/api/v1/auth/login", loginRequest);

            if (!response.IsSuccessStatusCode) return null;
            var user = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LoginResponse>(user);
        }

        public async Task<bool> RegisterAsync(RegisterRequest registerRequest)
        {
            var response = await httpClient.PostAsJsonAsync("/api/v1/auth/register", registerRequest);
            return response.IsSuccessStatusCode;
        }
    }
}
