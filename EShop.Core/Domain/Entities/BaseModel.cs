using System.ComponentModel.DataAnnotations;

namespace EShop.Core.Domain.Entities
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
