using Catalog.API.Models;

namespace Catalog.API.Services
{
  public interface IProductProvider
  {
    IEnumerable<Product> GetProducts();
  }
}
