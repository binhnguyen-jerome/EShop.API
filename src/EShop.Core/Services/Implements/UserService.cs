using EShop.Core.Domain.Entities;
using EShop.Core.Mappers;
using EShop.Core.Services.Interfaces;
using EShop.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EShop.Core.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<bool> RegisterUser(RegisterRequest registerRequest)
        {
            var User = new ApplicationUser
            {
                Email = registerRequest.Email,
                UserName = registerRequest.Email,
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                PhoneNumber = registerRequest.PhoneNumber,
                StreetAddress = registerRequest.StreetAddress,
                City = registerRequest.City,
                State = registerRequest.State,
                PostalCode = registerRequest.PostalCode
            };
            var result = await userManager.CreateAsync(User, registerRequest.Password);
            if (result.Succeeded)
            {
                if (registerRequest.Role == null)
                {
                    await userManager.AddToRoleAsync(User, "Customer");
                }
                await userManager.AddToRolesAsync(User, registerRequest.Role);
            }
            return result.Succeeded;
        }

        public async Task<bool> Login(LoginRequest loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.Email);
            if (user != null)
            {
                return await userManager.CheckPasswordAsync(user, loginRequest.Password);
            }
            return false;
        }
        public async Task<List<UserReponse>> GetAllUserAsync()
        {
            var users = await userManager.Users.ToListAsync();
            return users.Select(u => u.ToUserReponse()).ToList();
        }

        public async Task<UserReponse?> GetUserByIdAsync(Guid? id)
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
        public async Task<string> CreateJWTToken(LoginRequest user)
        {
            var claims = await GetClaims(user.Email);

            // After having identity information, we will configure using algorithms to encrypt 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // Create token 
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<List<Claim>> GetClaims(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
