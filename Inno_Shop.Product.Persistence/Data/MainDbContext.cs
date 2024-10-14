using Microsoft.EntityFrameworkCore;
namespace Inno_Shop.Product.Persistence.Data;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
        
    }
    public DbSet<Domain.Model.Product> Products { get; set; }
}