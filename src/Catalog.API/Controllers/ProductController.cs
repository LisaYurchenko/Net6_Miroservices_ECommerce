using Catalog.API.Models;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
  [ApiController]
  [Route("api/v1/[controller]")]
  public class ProductController : ControllerBase
  {

    private readonly ILogger<ProductController> _logger;

    private readonly IProductRepository _productRepository;

    public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
      var products = await _productRepository.GetProductsAsync();
      return Ok(products);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
      var product = await _productRepository.GetProductByIdAsync(id);
      if (product == null) return NotFound();

      return Ok(product);
    }

    [HttpGet]
    [Route("[action]/{category}")]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
    {
      var products = await _productRepository.GetProductsByCategoryAsync(category);
      return Ok(products);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
      await _productRepository.CreateProductAsync(product);

      return Created("CreateProduct", product);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
      return Ok(await _productRepository.UpdateProductAsync(product));
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProductById(string id)
    {
      return Ok(await _productRepository.DeleteProductAsync(id));
    }
  }
}
