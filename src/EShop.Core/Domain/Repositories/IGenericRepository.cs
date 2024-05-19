using System.Linq.Expressions;

namespace EShop.Core.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T> Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        void Add(T entity);
        void Remove(T entity);
        Task<IEnumerable<T>> RemoveRange(IEnumerable<T> entity);
        void Update(T entity);
    }
}
