using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Infrastucture.Data;

namespace EShop.Infrastucture.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category obj)
        {
            _context.Categories.Update(obj);
        }
    }
}
