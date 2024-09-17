using Inno_Shop.Authentification.Models;

namespace Inno_Shop.Authentification.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}