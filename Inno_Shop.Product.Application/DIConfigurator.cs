using Inno_Shop.Product.Application.AutoMapper;
using Inno_Shop.Product.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inno_Shop.Product.Application;

public static class DIConfigurator
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services,IConfiguration configuration)
    {
        
        services.AddDbContext<MainDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("MainConnection"));
        });
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        return services;
    }
}