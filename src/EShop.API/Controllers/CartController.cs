using EShop.Application.Services.Interfaces;
using EShop.ViewModels.Dtos.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/carts/")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {
        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("{applicationUserId:guid}")]
        public async Task<IActionResult> GetUserCarts([FromRoute] Guid applicationUserId)
        {
            var carts = await cartService.GetUserCartsAsync(applicationUserId);
            return Ok(carts);
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartRequest cartRequest)
        {
            var cart = await cartService.AddToCartAsync(cartRequest);
            return Ok(cart);
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveFromCart([FromRoute] Guid id)
        {
            var result = await cartService.RemoveFromCartAsync(id);
            return Ok(result);
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpPut]
        public async Task<IActionResult> UpdateCart([FromBody] CartRequest cartRequest)
        {
            var result = await cartService.UpdateCartAsync(cartRequest);
            return Ok(result);
        }

    }
}
