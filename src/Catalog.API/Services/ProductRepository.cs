using Catalog.API.Data;
using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Services
{
  public class ProductRepository : IProductRepository
  {
    private ICatalogContext _catalogContext;
    public ProductRepository(ICatalogContext catalogContext)
    {
      _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext)); ;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
      return await _catalogContext.Products.FindAsync(_ => true).Result.ToListAsync();
    }
    public async Task<Product> GetProductsByCategoryAsync(string category)
    {
      return await _catalogContext.Products.Find(p => p.Category == category).FirstOrDefaultAsync();
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
      return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Product> GetProductByNameAsync(string name)
    {
      return await _catalogContext.Products.Find(p => p.Name == name).FirstOrDefaultAsync();
    }
    public async Task CreateProductAsync(Product product)
    {
      await _catalogContext.Products.InsertOneAsync(product);
    }
    public async Task<bool> UpdateProductAsync(Product product)
    {
      var updateResult = await _catalogContext.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
      return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }
    public async Task<bool> DeleteProductAsync(string id)
    {
      var deleteResult = await _catalogContext.Products.DeleteOneAsync(filter: g => g.Id == id);
      return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
  }
}
