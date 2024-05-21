﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.ViewModels.ProductViewModel
{
    public class CreateProductRequest
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        public string? Summary { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceDiscount { get; set; }

        public int Stock { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public Guid CategoryId { get; set; }
        public List<ProductImageRequest> ProductImages { get; set; }
    }
    public class ProductImageRequest
    {
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
    }
    public class UpdateProductRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public string? Summary { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceDiscount { get; set; }

        public int Stock { get; set; }
        public DateTime? UpdateDate { get; set; } = DateTime.Now;

        public Guid CategoryId { get; set; }
        public List<ProductImageRequest> ProductImages { get; set; }
    }
}