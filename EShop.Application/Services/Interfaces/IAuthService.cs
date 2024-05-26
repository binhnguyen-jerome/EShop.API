using EShop.ViewModels.Dtos.User;

namespace EShop.Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserReponse> Login(LoginRequest loginRequest);
        Task<bool> RegisterUser(RegisterRequest registerRequest);
        Task<string> CreateJWTToken(LoginRequest user);
    }
}
