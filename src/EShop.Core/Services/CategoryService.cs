using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Core.IServices;
using EShop.Core.Mappers;
using EShop.ViewModels.CategoryViewModel;

namespace EShop.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<CategoryResponse> AddCategory(CategoryRequest categoryRequest)
        {
            if (categoryRequest == null)
                throw new ArgumentNullException(nameof(categoryRequest));
            Category category = categoryRequest.ToCategory();
            categoryRepository.Add(category);
            await unitOfWork.CompleteAsync();
            return category.ToCategoryResponse();

        }

        public async Task<CategoryResponse> DeleteCategory(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var category = await categoryRepository.Get(c => c.Id == id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            categoryRepository.Remove(category);
            await unitOfWork.CompleteAsync();
            return category.ToCategoryResponse();
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var categories = await categoryRepository.GetAll();
            return categories.Select(c => c.ToCategoryResponse()).ToList();
        }

        public async Task<CategoryResponse?> GetCategoryById(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var category = await categoryRepository.Get(c => c.Id == id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            return category?.ToCategoryResponse();
        }

        public async Task<CategoryResponse> UpdateCategory(Guid? id, CategoryRequest? categoryRequest)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (categoryRequest == null)
                throw new ArgumentNullException(nameof(categoryRequest));
            var category = await unitOfWork.Category.Get(c => c.Id == id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            category.Name = categoryRequest.Name;
            category.Description = categoryRequest.Description;
            unitOfWork.Category.Update(category);
            await unitOfWork.CompleteAsync();
            return category.ToCategoryResponse();

        }
    }
}
