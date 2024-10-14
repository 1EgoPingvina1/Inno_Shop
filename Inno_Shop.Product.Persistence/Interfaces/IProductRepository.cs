namespace Inno_Shop.Product.Persistence.Interfaces;

public interface IProductRepository
{
    Task<List<Domain.Model.Product>> GetAllAsync();
    Task<Domain.Model.Product?> GetByIdAsync(int id);
    void AddProduct(Domain.Model.Product product);
    void UpdateProduct(Domain.Model.Product product);
    void DeleteProduct(Domain.Model.Product product);
}