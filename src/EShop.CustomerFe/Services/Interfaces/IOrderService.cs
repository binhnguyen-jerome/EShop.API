using EShop.ViewModels.Dtos.Order;

namespace EShop.CustomerFe.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest);
    }
}
