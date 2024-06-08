using EShop.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EShop.Infrastructure.Data;

namespace EShop.Infrastructure.Repositories
{
    public sealed class GenericRepository<T>(ApplicationDbContext db) : IGenericRepository<T>
        where T : class
    {
        private readonly DbSet<T> dbSet = db.Set<T>();

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            query = tracked ? dbSet : dbSet.AsNoTracking();

            query = query.Where(filter);
            if (string.IsNullOrEmpty(includeProperties)) return await query.FirstOrDefaultAsync();
            foreach (var includeProp in includeProperties
                         .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (string.IsNullOrEmpty(includeProperties)) return await query.ToListAsync();
            foreach (var includeProp in includeProperties
                         .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
            return await query.ToListAsync();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
