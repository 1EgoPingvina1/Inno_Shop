using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Inno_Shop.Product.Service.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Token:Key") ?? string.Empty)),
                    ValidateIssuer = true,
                    ValidIssuer = "https://localhost:7127",
                    ValidateAudience = false
                };
            });
        return services;
    }
}