using EShop.Application.Services.Interfaces;
using EShop.ViewModels.Dtos.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/carts/")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("{applicationUserId}")]
        public async Task<IActionResult> GetUserCarts([FromRoute] Guid applicationUserId)
        {
            List<CartResponse> carts = await cartService.GetUserCartsAsync(applicationUserId);
            return Ok(carts);
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartRequest cartRequest)
        {
            CartResponse cart = await cartService.AddToCartAsync(cartRequest);
            return CreatedAtAction(nameof(AddToCart), new { id = cart.Id }, cart);
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart([FromRoute] Guid id)
        {
            bool result = await cartService.RemoveFromCartAsync(id);
            return Ok(result);
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpPut]
        public async Task<IActionResult> UpdateCart([FromBody] CartRequest cartRequest)
        {
            bool result = await cartService.UpdateCartAsync(cartRequest);
            return Ok(result);
        }

    }
}
