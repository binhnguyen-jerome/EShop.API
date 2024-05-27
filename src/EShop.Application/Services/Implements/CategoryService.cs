using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Core.Mappers;
using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.Category;
using Microsoft.Extensions.Logging;

namespace EShop.Core.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> categoryRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CategoryService> logger;

        public CategoryService(IUnitOfWork unitOfWork, ILogger<CategoryService> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            categoryRepository = unitOfWork.GetBaseRepo<Category>();
        }
        public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
        {
            logger.LogInformation("Get all categories");

            var categories = await categoryRepository.GetAll();
            return categories.Select(c => c.ToCategoryResponse()).ToList();
        }

        public async Task<CategoryResponse?> GetCategoryByIdAsync(Guid id)
        {
            logger.LogInformation("Get category by id");

            var category = await categoryRepository.Get(c => c.Id == id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            return category.ToCategoryResponse();
        }
        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest? categoryRequest)
        {
            logger.LogInformation("Create category");

            if (categoryRequest == null)
                throw new ArgumentNullException(nameof(categoryRequest));

            Category category = categoryRequest.ToCategory();
            categoryRepository.Add(category);
            await unitOfWork.CompleteAsync();
            return category.ToCategoryResponse();

        }

        public async Task<CategoryResponse> DeleteCategoryAsync(Guid id)
        {
            logger.LogInformation("Delete category");
            var category = await categoryRepository.Get(c => c.Id == id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            categoryRepository.Remove(category);
            await unitOfWork.CompleteAsync();
            return category.ToCategoryResponse();
        }

        public async Task<CategoryResponse> UpdateCategoryAsync(Guid id, CategoryRequest? categoryRequest)
        {
            logger.LogInformation("Update category");
            if (categoryRequest == null)
                throw new KeyNotFoundException(nameof(categoryRequest));
            var category = await categoryRepository.Get(c => c.Id == id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            category.Name = categoryRequest.Name;
            category.Description = categoryRequest.Description;
            categoryRepository.Update(category);
            await unitOfWork.CompleteAsync();
            return category.ToCategoryResponse();

        }
    }
}
