using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EShop.CustomerFe.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IUserService userService;

        public UserController(ILogger<ProductController> logger, IUserService userService)
        {
            _logger = logger;
            this.userService = userService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var token = await userService.AuthenticateAsync(loginRequest);
            if (token != null)
            {
                HttpContext.Session.SetString("Token", token);
                ViewBag.Token = token;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var result = await userService.RegisterAsync(registerRequest);
            if (result)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
