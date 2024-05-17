using EShop.ViewModels.UserViewModel;

namespace EShop.Core.IServices
{
    public interface IAuthService
    {
        Task<bool> Login(LoginRequest loginRequest);
        Task<bool> RegisterUser(RegisterRequest registerRequest);
        Task<string> CreateJWTToken(LoginRequest user);

    }
}
