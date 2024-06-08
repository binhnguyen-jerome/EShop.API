using EShop.Application.Mappers;
using EShop.Application.Services.Interfaces;
using EShop.Core.Domain.Entities;
using EShop.ViewModels.Dtos.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EShop.Core.Domain.Extensions;

namespace EShop.Application.Services.Implements
{
    public class AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        : IAuthService
    {
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
            if (!result.Succeeded) return result.Succeeded;
            await userManager.AddToRoleAsync(user, registerRequest.Role.ToString());
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
                    var userResponse = user.ToUserResponse();
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
        public async Task<string> CreateJwtToken(LoginRequest user)
        {
            var claims = await GetClaims(user.Email);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? string.Empty));

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
            var user = await userManager.FindByEmailAsync(email).ThrowIfNull($"Can not find {email}");
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
            };
            var roles = await userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            return claims;
        }
    }
}
