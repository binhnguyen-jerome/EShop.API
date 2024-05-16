using EShop.Core.Domain.Entities;

namespace EShop.Core.DTO.RequestDto
{
    public class CategoryRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public Category ToCategory()
        {
            return new Category
            {
                Name = Name,
                Description = Description
            };
        }
    }
}
