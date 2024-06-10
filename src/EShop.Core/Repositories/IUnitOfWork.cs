namespace EShop.Core.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        void Dispose();
        IGenericRepository<T> GetBaseRepo<T>() where T : class;
    }
}
