using EShop.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Data.Configuration
{
    public class ProductReviewImageConfiguration : IEntityTypeConfiguration<ProductReviewImage>
    {
        public void Configure(EntityTypeBuilder<ProductReviewImage> builder)
        {
            builder.HasOne(x => x.ProductReview)
                .WithMany(x => x.ProductReviewImages)
                .HasForeignKey(x => x.ProductReviewId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
