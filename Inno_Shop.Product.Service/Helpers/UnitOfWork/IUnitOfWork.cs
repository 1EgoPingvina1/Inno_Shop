namespace Inno_Shop.Product.Service.Helpers.UnitOfWork;

public interface IUnitOfWork
{
    Task<bool> Complete();
    bool HasChanges();
}