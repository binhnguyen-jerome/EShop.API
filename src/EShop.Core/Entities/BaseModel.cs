using System.ComponentModel.DataAnnotations;

namespace EShop.Core.Entities
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; } 
    }
}
