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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] Guid id)
        {
            var order = await orderService.GetOrderDetailByIdAsync(id);
            return Ok(order);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            var orderResponse = await orderService.CreateOrderAsync(orderRequest);
            return CreatedAtAction(nameof(CreateOrder), new { id = orderResponse }, orderResponse);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
        {
            var result = await orderService.DeleteOrderAsync(id);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, [FromRoute] OrderRequest orderRequest)
        {
            var orderResponse = await orderService.UpdateOrderAsync(id, orderRequest);
            return Ok(orderResponse);
        }

    }
}
