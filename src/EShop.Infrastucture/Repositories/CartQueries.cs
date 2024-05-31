using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastucture.Repositories
{
    public class CartQueries : BaseQuery<Cart>, ICartQueries
    {
        public CartQueries(ApplicationDbContext db) : base(db)
        { }

        public async Task<List<Cart>> GetUserCartsAsync(Guid applicationUserId)
        {
            return await dbSet
                .Where(c => c.ApplicationUserId == applicationUserId)
                .Include(c => c.Product)
                .ThenInclude(c => c.ProductImages)
                .ToListAsync();
        }
        public async Task<Cart?> GetByIdAsync(Guid id)
        {
            return await dbSet
                .Where(c => c.Id == id)
                .Include(c => c.Product)
                .ThenInclude(c => c.ProductImages)
                .FirstOrDefaultAsync();
        }
    }
}
