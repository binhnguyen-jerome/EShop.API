using EShop.Core.IServices;
using EShop.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/userManager")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _authService;

        public UserController(IUserService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var result = await _authService.RegisterUser(registerRequest);
            if (result)
            {
                return Ok("Successfull register user");
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (await _authService.Login(user))
            {
                var token = await _authService.CreateJWTToken(user);
                return Ok(token);
            };

            return BadRequest();
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _authService.GetAllUserAsync();
            return Ok(users);
        }
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _authService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _authService.DeleteUserAsync(id);
            if (result)
            {
                return Ok("User deleted");
            }
            return BadRequest();
        }
        [HttpPut("user/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserRequest updateUser)
        {
            UserReponse userReponse = await _authService.UpdateUserAsync(id, updateUser);
            return Ok(userReponse);

        }
    }
}
