using EShop.Core.Domain.Entities;

namespace EShop.Core.DTO.ResponseDto
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(CategoryResponse)) return false;

            CategoryResponse category = (CategoryResponse)obj;
            return Id == category.Id && Name == category.Name && Description == category.Description;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public static class CategoryExtensions
    {
        //Converts from Country object to CountryResponse object
        public static CategoryResponse ToCategoryResponse(this Category category)
        {
            return new CategoryResponse() { Id = category.Id, Name = category.Name, Description = category.Description };
        }
    }
}
