using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.Cart;
using EShop.ViewModels.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.CustomerFe.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartClientService cartService;
        private readonly IUserClientService userService;

        public CartController(ICartClientService cartService, IUserClientService userService)
        {
            this.cartService = cartService;
            this.userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }
            var carts = await cartService.GetCartByUserIdAsync(new Guid(userId));
            var cartVM = CartVM.Create(carts, new());
            return View(cartVM);
        }
        public async Task<IActionResult> Summary()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }
            var carts = await cartService.GetCartByUserIdAsync(new Guid(userId));
            var user = await userService.GetUserById(new Guid(userId));

            var cartVM = CartVM.Create(carts, new(), user);

            return View(cartVM);
        }
        [HttpPost]
        public async Task<IActionResult> Summary(CartVM cartVM)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }
            cartVM.CartItems = await cartService.GetCartByUserIdAsync(new Guid(userId));
            cartVM.OrderRequest.ApplicationUserId = new Guid(userId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int quantity)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var cartRequest = CartRequest.Create(userId, productId, quantity);

            var result = await cartService.AddToCartAsync(cartRequest);
            return Ok(result);
        }
        public async Task<IActionResult> Remove(Guid cartId)
        {
            var result = await cartService.RemoveFromCartAsync(cartId);
            if (!result)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> UpdateCart(Guid productId, int quantity)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartRequest = CartRequest.Create(userId, productId, quantity);

            var result = await cartService.UpdateCartAsync(cartRequest);
            if (!result)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }

}
