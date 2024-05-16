namespace EShop.Core.Domain.Entities
{
    public class Comment : BaseModel
    {
        public int Rate { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
