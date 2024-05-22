using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Core.Mappers;
using EShop.Core.Services.Interfaces;
using EShop.ViewModels.CategoryViewModel;

namespace EShop.Core.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            categoryRepository = unitOfWork.GetBaseRepo<Category>();
        }
        public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
        {
            var categories = await categoryRepository.GetAll();
            return categories.Select(c => c.ToCategoryResponse()).ToList();
        }

        public async Task<CategoryResponse?> GetCategoryByIdAsync(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var category = await categoryRepository.Get(c => c.Id == id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            return category.ToCategoryResponse();
        }
        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest categoryRequest)
        {
            if (categoryRequest == null)
                throw new ArgumentNullException(nameof(categoryRequest));
            Category category = categoryRequest.ToCategory();
            categoryRepository.Add(category);
            await unitOfWork.CompleteAsync();
            return category.ToCategoryResponse();

        }

        public async Task<CategoryResponse> DeleteCategoryAsync(Guid? id)
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

        public async Task<CategoryResponse> UpdateCategoryAsync(Guid? id, CategoryRequest? categoryRequest)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (categoryRequest == null)
                throw new ArgumentNullException(nameof(categoryRequest));
            var category = await categoryRepository.Get(c => c.Id == id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            category.Name = categoryRequest.Name;
            category.Description = categoryRequest.Description;
            categoryRepository.Update(category);
            await unitOfWork.CompleteAsync();
            return category.ToCategoryResponse();

        }
    }
}
