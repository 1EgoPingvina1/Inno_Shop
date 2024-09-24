namespace Inno_Shop.Product.Service.Interfaces;

public interface IAuthChecker
{
    Task<bool> IsUserAuthenticated();
}