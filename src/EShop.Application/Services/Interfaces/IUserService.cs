using EShop.ViewModels.Dtos.User;

namespace EShop.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserReponse>> GetUsersAsync();
        Task<UserReponse> GetUserAsync(int id);

        Task<bool> DeleteUserAsync(int id);
        Task<UserReponse> UpdateUserAsync(int id, UserRequest userRequest);
        Task<bool> UpdateUserRoleAsync(int id, string newRole);
    }
}
