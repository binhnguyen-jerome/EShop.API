namespace EShop.Core.Entities
{
    public class Cart : BaseModel
    {

        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
