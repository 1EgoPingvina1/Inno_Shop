namespace Inno_Shop.Product.Persistence.Helpers.UnitOfWork;

public interface IUnitOfWork
{
    Task<bool> Complete();
    bool HasChanges();
}