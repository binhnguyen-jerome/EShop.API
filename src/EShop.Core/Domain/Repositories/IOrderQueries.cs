using EShop.Core.Domain.Entities;

namespace EShop.Core.Domain.Repositories
{
    public interface IOrderQueries
    {
        Task<Order?> GetOrderDetailByIdAsync(Guid id);
        Task<List<Order>?> GetOrderByUserId(Guid userId);

    }
}
