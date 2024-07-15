namespace EShop.Core.Entities
{
    public class Rating : BaseModel
    {
        public int Rate { get; set; }
        public string? Content { get; set; }
        public DateTime CreateAt { get; set; }

        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
