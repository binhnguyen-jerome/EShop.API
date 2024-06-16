using System.ComponentModel.DataAnnotations;

namespace EShop.ViewModels.Dtos.Category
{
    public class CategoryRequest
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
