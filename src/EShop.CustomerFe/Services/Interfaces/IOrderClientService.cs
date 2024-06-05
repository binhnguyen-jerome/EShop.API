using EShop.ViewModels.Dtos.Order;

namespace EShop.CustomerFe.Services.Interfaces
{
    public interface IOrderClientService
    {
        Task<OrderResponse?> CreateOrderAsync(OrderRequest orderRequest);
    }
}
