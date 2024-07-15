namespace EShop.Core.Repositories.Generic
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        void Dispose();
        IGenericRepository<T> GetBaseRepo<T>() where T : class;
    }
}
