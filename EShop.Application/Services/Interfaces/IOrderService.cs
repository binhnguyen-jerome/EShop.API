using EShop.ViewModels.Dtos.Order;

namespace EShop.Core.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetAllOrderAsync();
        Task<OrderDetailResponse?> GetOrderDetailByIdAsync(Guid id);
        Task<OrderResponse> CreateOrderAsync(OrderRequest? order);
        Task<OrderResponse> UpdateOrderAsync(Guid id, OrderRequest? order);
        Task<bool> DeleteOrderAsync(Guid id);
    }
}
