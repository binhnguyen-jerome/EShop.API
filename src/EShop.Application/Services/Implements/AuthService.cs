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
            var existingUser = await userManager.FindByEmailAsync(registerRequest.Email);
            if (existingUser != null)
            {
                throw new ApplicationException("Email invalid");
            }
            var user = new ApplicationUser
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

            var result = await userManager.CreateAsync(user, registerRequest.Password);
            if (result.Succeeded)
            {
                if (registerRequest.Role == null)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
                await userManager.AddToRoleAsync(user, registerRequest.Role.ToString());
            }
            return result.Succeeded;
        }

        public async Task<UserReponse> Login(LoginRequest loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.Email);
            if (user != null)
            {
                var isPasswordCorrect = await userManager.CheckPasswordAsync(user, loginRequest.Password);
                if (isPasswordCorrect)
                {
                    var userResponse = user.ToUserReponse();
                    return userResponse;
                }
                else
                {
                    throw new ApplicationException("Invalid password");
                }
            }
            else
            {
                throw new KeyNotFoundException("User not found");
            }
        }
        public async Task<string> CreateJWTToken(LoginRequest user)
        {
            var claims = await GetClaims(user.Email);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
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
