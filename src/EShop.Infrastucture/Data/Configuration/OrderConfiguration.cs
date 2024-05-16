using EShop.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastucture.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                    .HasMany(o => o.OrderItems)
                    .WithOne(i => i.Order)
                    .HasForeignKey(i => i.OrderId)
                    .IsRequired();
            builder.HasOne(o => o.ApplicationUser)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.ApplicationUserId)
                    .IsRequired();
        }
    }
}
