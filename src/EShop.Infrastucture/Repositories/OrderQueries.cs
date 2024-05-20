using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastucture.Repositories
{
    public class OrderQueries : BaseQuery<Order>, IOrderQueries
    {
        public OrderQueries(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Order?> GetOrderDetailByIdAsync(Guid id)
        {
            return await dbSet
                 .Where(p => p.Id == id)
                 .Include(p => p.OrderItems)
                 .ThenInclude(p => p.Product)
                 .FirstOrDefaultAsync();
        }

        public async Task<List<Order>?> GetOrderByUserId(Guid userId)
        {
            return await dbSet
                .Where(p => p.ApplicationUserId == userId)
                .Include(p => p.OrderItems)
                .ThenInclude(p => p.Product)
                .ToListAsync();
        }
    }
}
