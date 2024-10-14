using Inno_Shop.Product.Persistence.Data;
using Inno_Shop.Product.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inno_Shop.Product.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MainDbContext _context;

    public ProductRepository(MainDbContext context) => _context = context;
    
    public async Task<List<Domain.Model.Product>> GetAllAsync() => await _context.Products.ToListAsync();
    public async Task<Domain.Model.Product?> GetByIdAsync(int id) => await _context.Products.SingleOrDefaultAsync(x => x.Id == id); 
    public void AddProduct(Domain.Model.Product product) =>  _context.Products.Add(product);
    public void UpdateProduct(Domain.Model.Product product) => _context.Products.Update(product);
    public void DeleteProduct(Domain.Model.Product product) => _context.Products.Remove(product);
}