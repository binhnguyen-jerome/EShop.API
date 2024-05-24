using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.User;
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
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _authService.Login(loginRequest);
            if (user != null)
            {
                var jwt = await _authService.CreateJWTToken(loginRequest);
                return Ok(new
                {
                    token = jwt,
                    userName = user.FirstName,
                    userId = user.Id
                });
            };

            return Unauthorized();
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
