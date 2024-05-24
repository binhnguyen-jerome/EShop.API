using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.User;
using Newtonsoft.Json;

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
        public async Task<LoginResponse> AuthenticateAsync(LoginRequest loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "/v1/userManager/login", loginRequest);

            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoginResponse>(user);
                //return token;
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
