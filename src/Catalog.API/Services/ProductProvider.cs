using Catalog.API.Data;
using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Services
{
  public class ProductProvider : IProductProvider
  {
    private ICatalogContext _catalogContext;
    public ProductProvider(ICatalogContext catalogContext)
    {
      _catalogContext = catalogContext;
    }

    public IEnumerable<Product> GetProducts()
    {
      return _catalogContext.Products.FindAsync(_ => true).Result.ToEnumerable<Product>();
    }
  }
}
