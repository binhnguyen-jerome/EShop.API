namespace EShop.Core.Entities;

public class CartItem : BaseModel
{
    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }
    public int Quantity { get; set; }
}