using EShop.Core.Repositories;
using EShop.Infrastructure.Data;

namespace EShop.Infrastructure.Repositories
{
    public class UnitOfWork(ApplicationDbContext db) : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new();

        public IGenericRepository<T> GetBaseRepo<T>() where T : class
        {
            if (_repositories.TryGetValue(typeof(T), out var repository))
            {
                return (IGenericRepository<T>)repository;
            }

            var newRepository = new GenericRepository<T>(db);
            _repositories[typeof(T)] = newRepository;
            return newRepository;
        }
        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}
