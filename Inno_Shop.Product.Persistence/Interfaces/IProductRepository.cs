namespace Inno_Shop.Product.Persistence.Interfaces;

public interface IProductRepository
{
    Task<List<Domain.Product>> GetAllAsync();
    Task<Domain.Product?> GetByIdAsync(int id);
    void AddProduct(Domain.Product product);
    Task UpdateProduct(Domain.Product product);
    Task DeleteProduct(Domain.Product product);
}