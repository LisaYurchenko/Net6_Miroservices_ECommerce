using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Data
{
  public class CatalogContext : ICatalogContext
  {

    public CatalogContext(IDatabaseSettings mongoConfig)
    {
      var client = new MongoClient(mongoConfig.ConnectionString);
      var db = client.GetDatabase(mongoConfig.DatabaseName);

      Products = db.GetCollection<Product>(mongoConfig.CollectionName);
      CatalogContextSeed.SeedData(Products);
    }

    public IMongoCollection<Product> Products { get; }
  }
}
