using System.Net;
using Catalog.API.Entities;
using Catalog.API.Localization;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route(Localizable.CatalogRouteConstant)]
public class CatalogController : ControllerBase
{
    private IProductRepository Repository { get; }

    private ILogger<CatalogController> Logger { get; }

    public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>),(int) HttpStatusCode.OK)]
    
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await Repository.GetAllProducts();
        return Ok(products);
    }
    
    [HttpGet("{id:length(24)}",Name = "GetProduct")]
    [ProducesResponseType(typeof(Product),(int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
        var product = await Repository.GetProduct(id);
        if (product == null)
        {
            Logger.LogError($"Product With Id {id} doesnt exist");
            return NotFound(id);
        }
        return Ok(product);
    }

    
    [HttpGet]
    [Route("category/{category}")]
    [ProducesResponseType(typeof(Product),(int) HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
    {
        var product = await Repository.GetProductsByCategory(category);
        return Ok(product);
    }
    [HttpGet("name/{name:required}")]
    [ProducesResponseType(typeof(IEnumerable<Product>),(int) HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
    {
        return Ok(await Repository.GetProductsByName(name));
    }
    
    
    
    [HttpPost]
    [ProducesResponseType(typeof(Product),(int) HttpStatusCode.OK)]
    public async Task<IActionResult> CreateProduct([FromQuery] Product product)
    {
        await Repository.CreateProduct(product);
        return CreatedAtRoute("GetProduct", new{id = product.Id},product);
    }
    

    
    [HttpPut]
    [ProducesResponseType(typeof(bool),(int) HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
        return Ok(await Repository.UpdateProduct(product));
    }

    [HttpDelete("{id:length(24)}",Name = "DeleteProduct")]
    [ProducesResponseType(typeof(bool),(int) HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProductById(string id)
    {
        return Ok(await Repository.DeleteProduct(id));
    }
    


}