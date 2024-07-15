using EShop.Core.Entities;

namespace EShop.Core.Repositories.Query
{
    public interface IOrderQueries
    {
        Task<Order?> GetOrderDetailByIdAsync(int id);
        Task<List<Order>?> GetOrderByUserId(int userId);

    }
}
