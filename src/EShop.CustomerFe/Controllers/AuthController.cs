using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.CustomerFe.Controllers
{
    public class AuthController(ILogger<ProductController> logger, IAuthClientService authService)
        : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = await authService.AuthenticateAsync(loginRequest);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
                    new Claim("token", user.token)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                logger.LogInformation("User {Name} logged in at {Time}", user.username, DateTime.UtcNow);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["LoginMessage"] = "Invalid login username or password is not correct";
                return View();
            }
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var result = await authService.RegisterAsync(registerRequest);
            if (result)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }
    }
}
