using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Inno_Shop.Product.API.Middleware;

public class AuthorizationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
        var authHeader = context.Request.Headers["Authorization"].ToString();
        var bearerIndex = authHeader.IndexOf("Bearer ", StringComparison.InvariantCulture);
        if (bearerIndex != -1)
        {
            var token = authHeader.Substring(bearerIndex + 7); // 7 is the length of "Bearer "
            if (!ValidateToken(token))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            context.Request.Headers["Authorization"] = token;
        }
        else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
        await next(context);
    }
    
    private bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParams = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false, // Set to false since you're not specifying an audience
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(".4tRL*8z*]7oY(NI^3MS$mdrn9}kVKXctn64vW/JVYuZf-E#gqqJB]<d-SkzvN+ee25204")),
            ValidIssuer = "AuthAPI"
        };
        try
        {
            tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
}