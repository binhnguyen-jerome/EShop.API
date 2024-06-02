using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.Cart;
using EShop.ViewModels.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.CustomerFe.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IUserService userService;

        public CartController(ICartService cartService, IUserService userService)
        {
            this.cartService = cartService;
            this.userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                RedirectToAction("Login", "User");
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
                RedirectToAction("Login", "User");
            }
            cartVM.CartItems = await cartService.GetCartByUserIdAsync(new Guid(userId));
            cartVM.OrderRequest.ApplicationUserId = new Guid(userId);
            return RedirectToAction("Index");
        }
        public async void AddToCart(Guid productId, int quantity)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                RedirectToAction("Login", "User");
            }
            var cartRequest = new CartRequest
            {
                ApplicationUserId = new Guid(userId),
                ProductId = productId,
                Quantity = quantity
            };
            await cartService.AddToCartAsync(cartRequest);
        }
        public async Task<bool> Remove(Guid cartId)
        {
            return await cartService.RemoveFromCartAsync(cartId);
        }
        public async Task<bool> Minus(Guid cartId)
        {
            return await cartService.MinusAsync(cartId);
        }
        public async Task<bool> Plus(Guid cartId)
        {
            return await cartService.PlusAsync(cartId);
        }
    }

}
