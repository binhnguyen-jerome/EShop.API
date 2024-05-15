using EShop.Core.Domain.Repositories;
using EShop.Infrastucture.Data;

namespace EShop.Infrastucture.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
        }
        public async Task CompleteAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
