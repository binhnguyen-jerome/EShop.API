using EShop.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastucture.Data.Configuration
{
    public class ProductReviewImageConfiguration : IEntityTypeConfiguration<ProductReviewImage>
    {
        public void Configure(EntityTypeBuilder<ProductReviewImage> builder)
        {
            builder.HasKey(x => new { x.ProductReviewId, x.ImageId });
            builder.HasOne(x => x.ProductReview)
                .WithMany(x => x.ProductReviewImages)
                .HasForeignKey(x => x.ProductReviewId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Image)
                .WithMany(x => x.ProductReviewImages)
                .HasForeignKey(x => x.ImageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
