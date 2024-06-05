using EShop.ViewModels.Dtos.User;

namespace EShop.CustomerFe.Services.Interfaces
{
    public interface IUserClientService
    {
        Task<UserReponse?> GetUserById(Guid userId);
    }
}
