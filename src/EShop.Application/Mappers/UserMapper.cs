using EShop.Core.Domain.Entities;
using EShop.ViewModels.Dtos.User;

namespace EShop.Application.Mappers
{
    public static class UserMapper
    {
        public static UserReponse ToUserResponse(this ApplicationUser user)
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
