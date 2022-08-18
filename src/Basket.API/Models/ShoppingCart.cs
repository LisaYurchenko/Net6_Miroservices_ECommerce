namespace Basket.API.Models
{
  public class ShoppingCart
  {
    public ShoppingCart()
    {

    }

    public ShoppingCart(string username)
    {

    }

    public string Username { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();


    public decimal Total => Items.Sum(x => x.Quantity * x.Price);
  }
}

