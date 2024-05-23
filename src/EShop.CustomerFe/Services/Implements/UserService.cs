using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.UserViewModel;

namespace EShop.CustomerFe.Services.Implements
{
    public class UserService : IUserService
    {
        Uri uri = new Uri("https://localhost:7045/api");
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = uri;
        }
        public async Task<string> AuthenticateAsync(LoginRequest loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "/v1/userManager/login", loginRequest);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                return token;
            }
            return null;
        }

        public async Task<bool> RegisterAsync(RegisterRequest registerRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "/v1/userManager/register", registerRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
