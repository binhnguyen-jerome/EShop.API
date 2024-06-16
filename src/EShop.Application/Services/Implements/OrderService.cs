using EShop.Application.Mappers;
using EShop.Application.Services.Interfaces;
using EShop.Core.Entities;
using EShop.Core.Exceptions;
using EShop.Core.Repositories;
using EShop.ViewModels.Dtos.Order;

namespace EShop.Application.Services.Implements
{
    public class OrderService(IUnitOfWork unitOfWork, IOrderQueries orderQueries) : IOrderService
    {
        private readonly IGenericRepository<Order> orderRepository = unitOfWork.GetBaseRepo<Order>();
        private readonly IGenericRepository<OrderItem> orderItem = unitOfWork.GetBaseRepo<OrderItem>();

        public async Task<List<OrderResponse>> GetAllOrderAsync()
        {
            var orders = await orderRepository.GetAllAsync();
            return orders.Select(x => x.ToOrderResponse()).ToList();
        }

        public async Task<OrderDetailResponse> GetOrderDetailByIdAsync(Guid id)
        {
            var order = await orderQueries.GetOrderDetailByIdAsync(id).ThrowIfNull($"Order with ID {id} not found");
            return order.ToOrderDetailResponse();
        }
        public async Task<OrderResponse> CreateOrderAsync(OrderRequest order)
        {
            var newOrder = order.ToCreateOrder();
            orderRepository.Add(newOrder);
            orderItem.AddRange(order.OrderItems.Select(x => new OrderItem
            {
                OrderId = newOrder.Id,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = x.Price
            }));
            await unitOfWork.CompleteAsync();

            return newOrder.ToOrderResponse();
        }

        public Task<OrderResponse> UpdateOrderAsync(Guid id, OrderRequest order)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            var order = await orderRepository.GetAsync(o => o.Id == id).ThrowIfNull($"Order with ID {id} not found"); ;
            orderRepository.Remove(order);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}
