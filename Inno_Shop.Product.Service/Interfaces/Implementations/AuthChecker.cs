using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Inno_Shop.Product.Service.Interfaces.Implementations;

public class AuthChecker : IAuthChecker
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthChecker(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> IsUserAuthenticated()
    {
        var HttpContext = _httpContextAccessor.HttpContext;
        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (token != null && token.StartsWith("Bearer "))
        {
            token = token.Substring(7);
            var jwtToken = new JwtSecurityToken(token);
            var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                // User is authorized, return true
                return true;
            }
        }
        // User is not authorized, return false
        return false;
    }
}