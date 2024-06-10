using EShop.Core.Entities;

namespace EShop.Core.Repositories
{
    public interface IOrderQueries
    {
        Task<Order?> GetOrderDetailByIdAsync(Guid id);
        Task<List<Order>?> GetOrderByUserId(Guid userId);

    }
}
