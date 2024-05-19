using Microsoft.AspNetCore.Identity;

namespace EShop.Core.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        public virtual ICollection<ProductReview>? ProductReviews { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }

    }
}
