using EShop.Application.Services.Interfaces;
using EShop.ViewModels.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/auth/")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var result = await authService.RegisterUser(registerRequest);
            if (result)
            {
                return Ok("Successfully register user");
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await authService.Login(loginRequest);
            if (true)
            {
                var jwt = await authService.CreateJwtToken(loginRequest);

                return Ok(new
                {
                    token = jwt,
                    userName = user.FirstName + " " + user.LastName,
                    userId = user.Id
                });
            };

            return Unauthorized();
        }
    }
}
