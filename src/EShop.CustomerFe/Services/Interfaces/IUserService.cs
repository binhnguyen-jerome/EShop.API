using EShop.ViewModels.Dtos.User;

namespace EShop.CustomerFe.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserReponse> GetUserById(Guid userId);
    }
}
