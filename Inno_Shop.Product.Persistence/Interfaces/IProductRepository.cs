namespace Inno_Shop.Product.Persistence.Interfaces;

public interface IProductRepository
{
    Task<List<Domain.Product>> GetAllAsync();
    Task<Domain.Product?> GetByIdAsync(int id);
    void AddProduct(Domain.Product product);
    void UpdateProduct(Domain.Product product);
    void DeleteProduct(Domain.Product product);
}