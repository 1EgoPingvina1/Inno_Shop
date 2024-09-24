using Inno_Shop.Authentification.Domain.Models;

namespace Inno_Shop.Authentification.Infrastructure.Security;

public interface ITokenService
{
    string CreateToken(User user);
}