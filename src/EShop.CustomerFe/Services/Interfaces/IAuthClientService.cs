using EShop.ViewModels.Dtos.User;

namespace EShop.CustomerFe.Services.Interfaces
{
    public interface IAuthClientService
    {
        Task<LoginResponse> AuthenticateAsync(LoginRequest loginRequest);
        Task<bool> RegisterAsync(RegisterRequest registerRequest);
    }
}
