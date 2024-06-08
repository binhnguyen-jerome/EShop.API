using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Repositories
{
    public class BaseQuery<T> where T : class
    {
        protected DbSet<T> dbSet;
        public BaseQuery(ApplicationDbContext db)
        {
            dbSet = db.Set<T>();
        }
    }
}
