using Inno_Shop.Product.Service.Data;

namespace Inno_Shop.Product.Service.Interfaces.Implementations;

public class ProductRepository : IProductRepository
{
    private MainDbContext _context;

    public ProductRepository(MainDbContext context)
    {
        _context = context;
    }

    public void AddProduct(Model.Product product)
    {
        _context.Products.Add(product);
    }

    public void UpdateProduct(Model.Product product)
    {
        throw new NotImplementedException();
    }

    public void DeleteProduct(Model.Product product)
    {
        throw new NotImplementedException();
    }
}