namespace Inno_Shop.Authentification.Domain.Interfaces;

public interface IForgetPasswordService
{
    void SaveForgetPassword(string userId,string token);
}