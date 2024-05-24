using EShop.Core.Domain.Entities;
using EShop.Core.Mappers;
using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EShop.Core.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
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
                await userManager.AddToRoleAsync(User, registerRequest.Role);
            }
            return result.Succeeded;
        }

        public async Task<UserReponse> Login(LoginRequest loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                var userResponse = user.ToUserReponse();
                return userResponse;
            }
            return null;
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
