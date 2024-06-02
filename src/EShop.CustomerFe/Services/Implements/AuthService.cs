using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.User;
using Newtonsoft.Json;

namespace EShop.CustomerFe.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LoginResponse> AuthenticateAsync(LoginRequest loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/v1/auth/login", loginRequest);

            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoginResponse>(user);
            }
            return null;
        }

        public async Task<bool> RegisterAsync(RegisterRequest registerRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/v1/auth/register", registerRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
