using EShop.Core.Domain.Entities;
using EShop.ViewModels.UserViewModel;

namespace EShop.Core.Mappers
{
    public static class UserMapper
    {
        public static UserReponse ToUserReponse(this ApplicationUser user)
        {
            return new UserReponse()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                StreetAddress = user.StreetAddress,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                PhoneNumber = user.PhoneNumber,
            };
        }

    }
}
