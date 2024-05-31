using EShop.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastucture.Data.Configuration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasOne(c => c.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(c => c.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.ApplicationUser)
                    .WithMany(u => u.Carts)
                    .HasForeignKey(c => c.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
