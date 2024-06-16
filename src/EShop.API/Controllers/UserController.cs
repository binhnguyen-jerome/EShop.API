using EShop.Application.Services.Interfaces;
using EShop.ViewModels.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/users/")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userService.GetUsersAsync();
            return Ok(users);
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await userService.GetUserAsync(id);
            return Ok(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var result = await userService.DeleteUserAsync(id);
            if (result)
            {
                return Ok("User deleted");
            }
            return BadRequest();
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserRequest updateUser)
        {
            var userResponse = await userService.UpdateUserAsync(id, updateUser);
            return Ok(userResponse);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}/role")]
        public async Task<IActionResult> UpdateUserRole([FromRoute] Guid id, [FromBody] string newRole)
        {
            var result = await userService.UpdateUserRoleAsync(id, newRole); 
            return Ok(result);
        }
    }
}
