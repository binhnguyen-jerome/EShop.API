using EShop.ViewModels.Dtos.User;

namespace EShop.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserReponse>> GetUsersAsync();
        Task<UserReponse> GetUserAsync(Guid id);

        Task<bool> DeleteUserAsync(Guid id);
        Task<UserReponse> UpdateUserAsync(Guid id, UserRequest userRequest);
        Task<bool> UpdateUserRoleAsync(Guid id, string newRole);
    }
}
