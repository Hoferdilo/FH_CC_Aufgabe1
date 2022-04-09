using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstAzureAPI.Model;

namespace MyFirstAzureAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly ProductContext _productContext;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ProductController> _logger;

    public ProductController(ProductContext productContext, IConfiguration configuration, ILogger<ProductController> logger)
    {
        _productContext = productContext;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        _logger.LogInformation($"Products have been read!");
        return Ok(await _productContext.Products.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        _logger.LogInformation($"Product with id {id} has been read!");
        return Ok(await _productContext.Products.FindAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> InsertProduct([FromBody] Product product)
    {
        if (!ValidateProduct(product))
        {
            _logger.LogWarning($"Product is invalid due to too high price!");
            return BadRequest(new { Message = "Invalid Product due to price!" });
        }
        product.Id = Guid.NewGuid();
        _productContext.Add(product);
        await _productContext.SaveChangesAsync();
        _logger.LogInformation($"Product successfully inserted!");
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var productToDelete = await _productContext.Products.FindAsync(id);
        if (productToDelete == null)
        {
            _logger.LogInformation($"Product could not be deleted, no product found!");
            return NotFound();
        }

        _productContext.Remove(productToDelete);
        await _productContext.SaveChangesAsync();
        _logger.LogInformation($"Product successfully deleted");
        return Ok(new { Id = productToDelete.Id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] Product product)
    {
        if (id != product.Id)
        {
            _logger.LogError($"Product update did not succeeed due to not matching ids.");
            BadRequest(new {message = "Product Ids do not match!"});
        }

        if (!ValidateProduct(product))
        {
            _logger.LogError($"Product invalid due to too high price.");
            return BadRequest(new { message = "Product invalid due to to high price!" });
        }

        try
        {
            _productContext.Entry(product).State = EntityState.Modified;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _productContext.Products.AnyAsync(x => x.Id == id))
            {
                _logger.LogError($"Product with id {id} was not found");
                return NotFound();
            }
            else
            {
                throw;
            }
        } 
        await _productContext.SaveChangesAsync();
        return Ok(product);
    }

    private bool ValidateProduct(Product product)
    {
        return product.Price <= _configuration.GetValue<decimal>("ProductMaxPrice");
    }
}