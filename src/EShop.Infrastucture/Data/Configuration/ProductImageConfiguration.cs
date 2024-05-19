using EShop.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastucture.Data.Configuration
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(p => new { p.ProductId, p.ImageId });

            builder.HasOne(p => p.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Image)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(p => p.ImageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
