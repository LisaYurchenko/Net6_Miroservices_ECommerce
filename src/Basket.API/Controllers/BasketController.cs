using Basket.API.Data;
using Basket.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
  [ApiController]
  [Route("api/v1/[controller]")]
  public class BasketController : ControllerBase
  {
    IBasketRepository _basketRepo;

    public BasketController(IBasketRepository basketRepo)
    {
      _basketRepo = basketRepo ?? throw new ArgumentNullException(nameof(basketRepo));
    }

    [HttpGet]
    [Route("{username}")]
    [ProducesResponseType(typeof(ActionResult<ShoppingCart?>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart?>> GetBasket(string username)
    {
      var basket = await _basketRepo.GetBasketAsync(username);
      if (basket == null)
      {
        return new ShoppingCart(username);
      }
      return Ok(basket);
    }

    [HttpPut]
    [ProducesResponseType(typeof(ActionResult<ShoppingCart?>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ShoppingCart?>> UpdateBasket([FromBody] ShoppingCart basket)
    {
      var newBasket = await _basketRepo.UpdateBasket(basket);
      if (basket == null)
      {
        return NotFound();
      }
      return Ok(newBasket);
    }

    [HttpDelete]
    [Route("{username}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> UpdateBasket(string username)
    {
      await _basketRepo.DeleteBasket(username);
      return Ok();
    }
  }
}
