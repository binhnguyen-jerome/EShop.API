using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Core.Mappers;
using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.Order;

namespace EShop.Core.Services.Implements
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOrderQueries orderQueries;
        private readonly IGenericRepository<Order> orderRepository;
        private readonly IGenericRepository<OrderItem> orderItem;
        public OrderService(IUnitOfWork unitOfWork, IOrderQueries orderQueries)
        {
            this.unitOfWork = unitOfWork;
            this.orderQueries = orderQueries;
            orderRepository = unitOfWork.GetBaseRepo<Order>();
            orderItem = unitOfWork.GetBaseRepo<OrderItem>();
        }

        public async Task<List<OrderResponse>> GetAllOrderAsync()
        {
            var orders = await orderRepository.GetAll();
            return orders.Select(x => x.ToOrderResponse()).ToList();
        }

        public async Task<OrderDetailResponse?> GetOrderDetailByIdAsync(Guid id)
        {
            var order = await orderQueries.GetOrderDetailByIdAsync(id);
            if (order == null)
                throw new KeyNotFoundException(nameof(order));
            return order.ToOrderDetailResponse();
        }
        public async Task<OrderResponse> CreateOrderAsync(OrderRequest? order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            Order newOrder = order.ToCreateOrder();
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
            var order = await orderRepository.Get(o => o.Id == id);
            if (order == null)
                throw new KeyNotFoundException(nameof(order));
            orderRepository.Remove(order);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}
