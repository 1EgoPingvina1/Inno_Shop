using System.Text;
using Inno_Shop.Authentification.Domain.Models;
using Inno_Shop.Authentification.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Inno_Shop.Authentification.Application.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection InjectIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")),ServiceLifetime.Transient);

        services.AddIdentityCore<User>(options => { })
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>()
            .AddEntityFrameworkStores<IdentityContext>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Token:Key") ?? string.Empty)),
                    ValidIssuer = configuration.GetValue<string>("Token:Issuer"),
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });
        return services;
    }
}