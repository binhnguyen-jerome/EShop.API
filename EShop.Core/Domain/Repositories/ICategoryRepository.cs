using EShop.Core.Domain.Entities;

namespace EShop.Core.Domain.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {

        void Update(Category obj);
    }
}
