using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Data
{
  public class BasketRepository : IBasketRepository
  {
    private readonly IDistributedCache _redisCache;

    public BasketRepository(IDistributedCache cache)
    {
      _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<ShoppingCart?> GetBasketAsync(string username)
    {
      var basket = await _redisCache.GetStringAsync(username);
      if (string.IsNullOrEmpty(basket))
      {
        return null;
      }

      return JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart?> UpdateBasket(ShoppingCart basket)
    {
      await _redisCache.SetStringAsync(basket.Username, JsonConvert.SerializeObject(basket));
      return await GetBasketAsync(basket.Username);
    }

    public async Task DeleteBasket(string username)
    {
      await _redisCache.RemoveAsync(username);
    }
  }
}
