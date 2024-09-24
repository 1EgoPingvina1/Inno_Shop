using Microsoft.EntityFrameworkCore;
namespace Inno_Shop.Product.Service.Data;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
        
    }
    public DbSet<Model.Product> Products { get; set; }
}