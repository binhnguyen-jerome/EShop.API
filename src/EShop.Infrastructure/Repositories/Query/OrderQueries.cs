using EShop.Core.Entities;
using EShop.Core.Repositories;
using EShop.Core.Repositories.Query;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastucture.Repositories.Query
{
    public class OrderQueries(ApplicationDbContext db) : BaseQuery<Order>(db), IOrderQueries
    {
        public async Task<Order?> GetOrderDetailByIdAsync(int id)
        {
            return await dbSet
                 .Where(p => p.Id == id)
                 .Include(p => p.OrderItems)
                 .ThenInclude(p => p.Product)
                 .FirstOrDefaultAsync();
        }

        public async Task<List<Order>?> GetOrderByUserId(int userId)
        {
            return await dbSet
                .Where(p => p.ApplicationUserId == userId)
                .Include(p => p.OrderItems)
                .ThenInclude(p => p.Product)
                .ToListAsync();
        }
    }
}
