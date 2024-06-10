using EShop.Application.Mappers;
using EShop.Application.Services.Interfaces;
using EShop.Core.Entities;
using EShop.Core.Exceptions;
using EShop.Core.Repositories;
using EShop.ViewModels.Dtos.Category;

namespace EShop.Application.Services.Implements
{
    public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
    {
        private readonly IGenericRepository<Category> categoryRepository = unitOfWork.GetBaseRepo<Category>();


        public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
        {
            var categories = await categoryRepository.GetAllAsync();
            return categories.Select(c => c.ToCategoryResponse()).ToList();
        }

        public async Task<CategoryResponse> GetCategoryByIdAsync(Guid id)
        {
            var category = await categoryRepository.GetAsync(c => c.Id == id).ThrowIfNull($"Category with ID {id} not found");
            return category.ToCategoryResponse();
        }
        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest categoryRequest)
        {
            var category = categoryRequest.ToCategory();
            categoryRepository.Add(category);
            await unitOfWork.CompleteAsync();
            return category.ToCategoryResponse();

        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var category = await categoryRepository.GetAsync(c => c.Id == id).ThrowIfNull($"Category with ID {id} not found");
            categoryRepository.Remove(category);
            await unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<CategoryResponse> UpdateCategoryAsync(Guid id, CategoryRequest categoryRequest)
        {
            var category = await categoryRepository.GetAsync(c => c.Id == id).ThrowIfNull($"Category with ID {id} not found");

            category.Name = categoryRequest.Name;
            category.Description = categoryRequest.Description;
            categoryRepository.Update(category);

            await unitOfWork.CompleteAsync();
            return category.ToCategoryResponse();

        }
    }
}
