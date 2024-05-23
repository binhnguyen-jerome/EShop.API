using EShop.ViewModels.UserViewModel;

namespace EShop.CustomerFe.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(LoginRequest loginRequest);
        Task<bool> RegisterAsync(RegisterRequest registerRequest);
    }
}
