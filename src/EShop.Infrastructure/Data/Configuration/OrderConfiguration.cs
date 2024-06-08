using EShop.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.HasOne(o => o.ApplicationUser)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
