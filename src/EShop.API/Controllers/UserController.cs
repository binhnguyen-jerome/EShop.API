using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.User;
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
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userService.GetUsersAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await userService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await userService.DeleteUserAsync(id);
            if (result)
            {
                return Ok("User deleted");
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserRequest updateUser)
        {
            UserReponse userReponse = await userService.UpdateUserAsync(id, updateUser);
            return Ok(userReponse);

        }
    }
}
