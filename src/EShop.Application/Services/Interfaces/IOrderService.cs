using EShop.ViewModels.Dtos.Order;

namespace EShop.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetAllOrderAsync();
        Task<OrderResponse> CreateOrderAsync(OrderRequest order);
        Task<OrderResponse> UpdateOrderAsync(int id, OrderRequest order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
