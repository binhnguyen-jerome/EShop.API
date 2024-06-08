using EShop.ViewModels.Dtos.User;

namespace EShop.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserReponse> Login(LoginRequest loginRequest);
        Task<bool> RegisterUser(RegisterRequest registerRequest);
        Task<string> CreateJwtToken(LoginRequest user);
    }
}
