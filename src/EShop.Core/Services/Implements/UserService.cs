using EShop.Core.Domain.Entities;
using EShop.Core.Mappers;
using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<List<UserReponse>> GetUsersAsync()
        {
            var users = await userManager.Users.ToListAsync();
            return users.Select(u => u.ToUserReponse()).ToList();
        }
        public async Task<UserReponse?> GetUserAsync(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return user.ToUserReponse();
        }
        public async Task<bool> DeleteUserAsync(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var user = userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await userManager.DeleteAsync(user.Result);
            return true;
        }
        public async Task<UserReponse> UpdateUserAsync(Guid? id, UserRequest userRequest)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.Email = userRequest.Email;
            user.FirstName = userRequest.FirstName;
            user.LastName = userRequest.LastName;
            user.PhoneNumber = userRequest.PhoneNumber;
            user.StreetAddress = userRequest.StreetAddress;
            user.City = userRequest.City;
            user.State = userRequest.State;
            user.PostalCode = userRequest.PostalCode;
            await userManager.UpdateAsync(user);
            return user.ToUserReponse();
        }
    }
}
