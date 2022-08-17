using Catalog.API.Models;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CatalogController : ControllerBase
  {

    private readonly ILogger<CatalogController> _logger;

    private readonly IProductProvider _productProvider;

    public CatalogController(ILogger<CatalogController> logger, IProductProvider productProvider)
    {
      _logger = logger;
      _productProvider = productProvider;
    }

    [HttpGet]
    public ActionResult<Product> Get()
    {
      return Ok(_productProvider.GetProducts());
    }
  }
}
