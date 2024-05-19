namespace EShop.Core.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        Task CompleteAsync();
        void Dispose();
        IGenericRepository<T> GetBaseRepo<T>() where T : class;
    }
}
