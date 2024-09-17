namespace Inno_Shop.Product.Service.Interfaces;

public interface IProductRepository
{
    
    void AddProduct(Model.Product product);
    void UpdateProduct(Model.Product product);
    void DeleteProduct(Model.Product product);
}