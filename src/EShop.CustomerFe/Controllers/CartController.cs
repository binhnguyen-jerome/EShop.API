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
            CartVM cartVM = new()
            {
                CartItems = carts,
            };
            return View(cartVM);
        }
        public async Task<IActionResult> Summary()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            CartVM cartVM = new()
            {
                CartItems = await cartService.GetCartByUserIdAsync(new Guid(userId)),
                OrderRequest = new(),
            };
            var user = await userService.GetUserById(new Guid(userId));
            cartVM.OrderRequest.FirstName = user.FirstName;
            cartVM.OrderRequest.LastName = user.LastName;
            cartVM.OrderRequest.PhoneNumber = user.PhoneNumber;
            cartVM.OrderRequest.StreetAddress = user.StreetAddress;
            cartVM.OrderRequest.PostalCode = user.PostalCode;
            cartVM.OrderRequest.City = user.City;
            cartVM.OrderRequest.OrderTotal = cartVM.TotalPrice;
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
                RedirectToAction("Login", "Auth");
            }
            var cartRequest = new CartRequest
            {
                ApplicationUserId = new Guid(userId),
                ProductId = productId,
                Quantity = quantity
            };
            var result = await cartService.AddToCartAsync(cartRequest);
            return Ok(result);
        }
        [HttpDelete]
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
            var cartRequest = new CartRequest
            {
                ApplicationUserId = new Guid(userId),
                ProductId = productId,
                Quantity = quantity
            };
            var result = await cartService.UpdateCartAsync(cartRequest);
            if (!result)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }

}
