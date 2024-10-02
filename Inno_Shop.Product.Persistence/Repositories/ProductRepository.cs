using Inno_Shop.Product.Persistence.Data;
using Inno_Shop.Product.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inno_Shop.Product.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MainDbContext _context;

    public ProductRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<List<Domain.Product>> GetAllAsync() => await _context.Products.ToListAsync();
    
    public async Task<Domain.Product?> GetByIdAsync(int id) => await _context.Products.SingleOrDefaultAsync(x => x.Id == id); 
    
    public void AddProduct(Domain.Product product) =>  _context.Products.Add(product);
    
    public async Task UpdateProduct(Domain.Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProduct(Domain.Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}