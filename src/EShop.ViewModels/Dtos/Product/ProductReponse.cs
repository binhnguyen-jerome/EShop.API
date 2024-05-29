using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.ViewModels.ProductViewModel
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
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
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProductImageResponse>? Images { get; set; }
    }
    public class ProductImageResponse
    {
        public string ImageUrl { get; set; }
    }
}
