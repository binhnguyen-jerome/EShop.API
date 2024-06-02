using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/users/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userService.GetUsersAsync();
            return Ok(users);
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await userService.GetUserAsync(id);
            return Ok(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserRequest updateUser)
        {
            UserReponse userReponse = await userService.UpdateUserAsync(id, updateUser);
            return Ok(userReponse);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateUserRole([FromRoute] Guid id, [FromBody] string newRole)
        {
            var result = await userService.UpdateUserRoleAsync(id, newRole);
            if (result)
            {
                return Ok("Role updated");
            }
            return BadRequest();
        }
    }
}
