using EShop.Core.IServices;
using EShop.ViewModels.OrderViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/v1/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            List<OrderResponse> orders = await orderService.GetAllOrderAsync();
            return Ok(orders);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            OrderDetailResponse? order = await orderService.GetOrderDetailByIdAsync(id);
            return Ok(order);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(OrderRequest orderRequest)
        {
            OrderResponse orderResponse = await orderService.CreateOrderAsync(orderRequest);
            return CreatedAtAction(nameof(Create), new { id = orderResponse }, orderResponse);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await orderService.DeleteOrderAsync(id);
            return Ok(result);
        }

    }
}
