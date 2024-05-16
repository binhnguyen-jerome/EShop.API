using EShop.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastucture.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            builder
                    .HasMany(u => u.Orders)
                    .WithOne(o => o.ApplicationUser)
                    .HasForeignKey(o => o.Id)
                    .IsRequired();
            builder
                    .HasMany(u => u.Comments)
                    .WithOne(c => c.ApplicationUser)
                    .HasForeignKey(c => c.Id)
                    .IsRequired();
        }
    }
}
