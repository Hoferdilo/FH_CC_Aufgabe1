using Microsoft.EntityFrameworkCore;

namespace MyFirstAzureAPI.Model;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }
}