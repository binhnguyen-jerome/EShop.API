using EShop.ViewModels.UserViewModel;

namespace EShop.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Login(LoginRequest loginRequest);
        Task<bool> RegisterUser(RegisterRequest registerRequest);
        Task<string> CreateJWTToken(LoginRequest user);
        Task<List<UserReponse>> GetAllUserAsync();
        Task<UserReponse?> GetUserByIdAsync(Guid? id);

        Task<bool> DeleteUserAsync(Guid? id);
        Task<UserReponse> UpdateUserAsync(Guid? id, UserRequest userRequest);
    }
}
