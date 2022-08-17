using Catalog.API.Models;

namespace Catalog.API.Services
{
  public interface IProductRepository
  {
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(string id);
    Task<Product> GetProductByNameAsync(string name);
    Task<Product> GetProductsByCategoryAsync(string category);

    Task CreateProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(string id);
  }
}
