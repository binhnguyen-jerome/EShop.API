using EShop.Core.Entities;
using EShop.Core.Repositories.Query;
using EShop.Infrastructure.Data;

namespace EShop.Infrastucture.Repositories.Query;

public class CartQueries (ApplicationDbContext db) : BaseQuery<Cart>(db), ICartQueries
{
 public Task<List<Cart>> GetUserCartsAsync(int applicationUserId)
 {
  throw new NotImplementedException();
 }

 public Task<Cart?> GetCartByIdAsync(int cartId)
 {
  throw new NotImplementedException();
 }

 public Task<CartItem?> GetCartItemByUserIdAndProductIdAsync(int applicationUserId, int productId)
 {
  throw new NotImplementedException();
 }

 public Task<Cart?> GetCartByUserIdAsync(int applicationUserId)
 {
  throw new NotImplementedException();
 }
}