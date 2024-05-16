using EShop.Core.Domain.Entities;
using EShop.ViewModels.CategoryViewModel;

namespace EShop.Core.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryResponse ToCategoryResponse(this Category category)
        {
            return new CategoryResponse() { Id = category.Id, Name = category.Name, Description = category.Description };
        }
        public static Category ToCategory(this CategoryRequest categoryRequest)
        {
            return new Category
            {
                Name = categoryRequest.Name,
                Description = categoryRequest.Description
            };
        }
    }
}
