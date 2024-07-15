using EShop.Application.Services.Interfaces;
using EShop.ViewModels.Dtos.Order;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/orders/")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await orderService.GetAllOrderAsync();
            return Ok(orders);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            var result = await orderService.DeleteOrderAsync(id);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromRoute] OrderRequest orderRequest)
        {
            var orderResponse = await orderService.UpdateOrderAsync(id, orderRequest);
            return Ok(orderResponse);
        }

    }
}
