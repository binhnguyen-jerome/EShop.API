namespace EShop.Core.Domain.Repositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        Task CompleteAsync();
    }
}
