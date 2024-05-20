using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Core.IServices;
using EShop.Core.Mappers;
using EShop.ViewModels.OrderViewModel;

namespace EShop.Core.Services
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

        public async Task<OrderDetailResponse?> GetOrderDetailByIdAsync(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var order = await orderQueries.GetOrderDetailByIdAsync(id.Value);
            if (order == null)
                throw new ArgumentNullException(nameof(order));
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
            }));
            await unitOfWork.CompleteAsync();
            return newOrder.ToOrderResponse();

        }
        public Task<OrderResponse> UpdateOrderAsync(Guid? id, OrderRequest? order)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteOrderAsync(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var order = await orderRepository.Get(o => o.Id == id);
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            orderRepository.Remove(order);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}
