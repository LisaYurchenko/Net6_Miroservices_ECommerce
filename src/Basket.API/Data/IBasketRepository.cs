using Basket.API.Models;

namespace Basket.API.Data
{
  public interface IBasketRepository
  {
    Task DeleteBasket(string username);
    Task<ShoppingCart?> GetBasketAsync(string username);
    Task<ShoppingCart?> UpdateBasket(ShoppingCart basket);
  }
}
