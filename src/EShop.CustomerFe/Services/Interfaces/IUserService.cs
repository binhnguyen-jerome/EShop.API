using EShop.ViewModels.Dtos.User;

namespace EShop.CustomerFe.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> AuthenticateAsync(LoginRequest loginRequest);
        Task<bool> RegisterAsync(RegisterRequest registerRequest);
    }
}
