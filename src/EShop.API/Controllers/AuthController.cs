using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var result = await authService.RegisterUser(registerRequest);
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
            var user = await authService.Login(loginRequest);
            if (user != null)
            {
                var jwt = await authService.CreateJWTToken(loginRequest);
                return Ok(new
                {
                    token = jwt,
                    userName = user.FirstName,
                    userId = user.Id
                });
            };

            return Unauthorized();
        }
    }
}
