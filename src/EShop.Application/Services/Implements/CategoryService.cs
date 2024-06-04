using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Extensions;
using EShop.Core.Domain.Repositories;
using EShop.Core.Mappers;
using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.Category;
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

        public async Task<CategoryResponse> GetCategoryByIdAsync(Guid id)
        {
            var category = await categoryRepository.Get(c => c.Id == id).ThrowIfNull($"Category with ID {id} not found");
            return category.ToCategoryResponse();
        }
        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest categoryRequest)
        {
            Category category = categoryRequest.ToCategory();
            categoryRepository.Add(category);
            await unitOfWork.CompleteAsync();
            return category.ToCategoryResponse();

        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var category = await categoryRepository.Get(c => c.Id == id).ThrowIfNull($"Category with ID {id} not found");
            categoryRepository.Remove(category);
            await unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<CategoryResponse> UpdateCategoryAsync(Guid id, CategoryRequest categoryRequest)
        {
            var category = await categoryRepository.Get(c => c.Id == id).ThrowIfNull($"Category with ID {id} not found");

            category.Name = categoryRequest.Name;
            category.Description = categoryRequest.Description;
            categoryRepository.Update(category);

            await unitOfWork.CompleteAsync();
            return category.ToCategoryResponse();

        }
    }
}
