using System.ComponentModel.DataAnnotations;

namespace EShop.Core.Domain.Entities
{
    public class Category : BaseModel
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
