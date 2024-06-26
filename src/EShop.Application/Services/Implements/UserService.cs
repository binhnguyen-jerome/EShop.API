﻿using EShop.Application.Mappers;
using EShop.Application.Services.Interfaces;
using EShop.Core.Entities;
using EShop.Core.Exceptions;
using EShop.ViewModels.Dtos.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Services.Implements
{
    public class UserService(UserManager<ApplicationUser> userManager) : IUserService
    {
        public async Task<List<UserReponse>> GetUsersAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var userResponses = new List<UserReponse>();
            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                userResponses.Add(new UserReponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    StreetAddress = user.StreetAddress,
                    City = user.City,
                    State = user.State,
                    PostalCode = user.PostalCode,
                    Role = roles.FirstOrDefault(),
                });
            }
            return userResponses;
        }
        public async Task<UserReponse> GetUserAsync(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString()).ThrowIfNull($"User with ID {id} not found");

            var roles = await userManager.GetRolesAsync(user);
            return new UserReponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                StreetAddress = user.StreetAddress,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                Role = roles.FirstOrDefault(),
            };
        }
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = userManager.FindByIdAsync(id.ToString()).ThrowIfNull($"User with ID {id} not found");

            await userManager.DeleteAsync(user.Result);
            return true;
        }
        public async Task<UserReponse> UpdateUserAsync(Guid id, UserRequest userRequest)
        {
            var user = await userManager.FindByIdAsync(id.ToString()).ThrowIfNull($"User with ID {id} not found");

            user.Email = userRequest.Email;
            user.FirstName = userRequest.FirstName;
            user.LastName = userRequest.LastName;
            user.PhoneNumber = userRequest.PhoneNumber;
            user.StreetAddress = userRequest.StreetAddress;
            user.City = userRequest.City;
            user.State = userRequest.State;
            user.PostalCode = userRequest.PostalCode;

            await userManager.UpdateAsync(user);
            return user.ToUserResponse();
        }
        public async Task<bool> UpdateUserRoleAsync(Guid id, string newRole)
        {
            var user = await userManager.FindByIdAsync(id.ToString()).ThrowIfNull($"User with ID {id} not found");

            var currentRoles = await userManager.GetRolesAsync(user);
            var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                throw new InvalidOperationException("Failed to remove user roles");
            }

            var addResult = await userManager.AddToRoleAsync(user, newRole.ToString());
            if (!addResult.Succeeded)
            {
                throw new InvalidOperationException("Failed to add user to new role");
            }

            return true;
        }
    }
}
