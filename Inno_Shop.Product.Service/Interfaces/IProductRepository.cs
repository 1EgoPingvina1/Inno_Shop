using Inno_Shop.Product.Service.DTO;

namespace Inno_Shop.Product.Service.Interfaces;

public interface IProductRepository
{
    Task<List<Model.Product>> GetAllAsync();
    Task<Model.Product?> GetByIdAsync(int id);
    void AddProduct(Model.Product product);
    Task UpdateProduct(Model.Product product);
    Task DeleteProduct(Model.Product product);
}