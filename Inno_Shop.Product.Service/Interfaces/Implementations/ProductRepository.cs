using Inno_Shop.Product.Service.Data;
using Inno_Shop.Product.Service.DTO;
using Microsoft.EntityFrameworkCore;

namespace Inno_Shop.Product.Service.Interfaces.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly MainDbContext _context;

    public ProductRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<List<Model.Product>> GetAllAsync() => await _context.Products.ToListAsync();
    
    public async Task<Model.Product?> GetByIdAsync(int id) => await _context.Products.SingleOrDefaultAsync(x => x.Id == id); 
    
    public void AddProduct(Model.Product product) =>  _context.Products.Add(product);
    
    public async Task UpdateProduct(Model.Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProduct(Model.Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();    }
}