using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Core.Entities
{
    public class OrderItem : BaseModel
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
