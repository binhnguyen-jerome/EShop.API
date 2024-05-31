using EShop.Application.Services.Interfaces;
using EShop.ViewModels.Dtos.Cart;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/cart/")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserCarts([FromQuery] Guid applicationUserId)
        {
            List<CartResponse> carts = await cartService.GetUserCartsAsync(applicationUserId);
            return Ok(carts);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartRequest cartRequest)
        {
            CartResponse cart = await cartService.AddToCartAsync(cartRequest);
            return CreatedAtAction(nameof(AddToCart), new { id = cart.Id }, cart);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart([FromRoute] Guid? id)
        {
            bool result = await cartService.RemoveFromCartAsync(id.Value);
            return Ok(result);
        }
        [HttpPut("minus/{id}")]
        public async Task<IActionResult> Minus([FromRoute] Guid? id)
        {
            bool result = await cartService.MinusAsync(id.Value);
            return Ok(result);
        }
        [HttpPut("plus/{id}")]
        public async Task<IActionResult> Plus([FromRoute] Guid? id)
        {
            bool result = await cartService.PlusAsync(id.Value);
            return Ok(result);
        }

    }
}
