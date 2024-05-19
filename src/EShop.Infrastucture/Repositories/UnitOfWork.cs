using EShop.Core.Domain.Repositories;
using EShop.Infrastucture.Data;

namespace EShop.Infrastucture.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly Dictionary<Type, object> _repositories = new();
        public IProductRepository Product { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Product = new ProductRepository(_db);
        }
        public IGenericRepository<T> GetBaseRepo<T>() where T : class
        {
            if (_repositories.TryGetValue(typeof(T), out var repository))
            {
                return (IGenericRepository<T>)repository;
            }

            var newRepository = new GenericRepository<T>(_db);
            _repositories[typeof(T)] = newRepository;
            return newRepository;
        }
        public async Task CompleteAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
