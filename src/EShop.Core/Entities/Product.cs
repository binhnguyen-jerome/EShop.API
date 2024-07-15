using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Core.Entities
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public string? Summary { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceDiscount { get; set; }

        public int Stock { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public virtual ICollection<ProductImage>? ProductImages { get; set; }
        public virtual ICollection<Rating>? ProductReviews { get; set; }

        public virtual ICollection<Cart>? Carts { get; set; }

    }
}
