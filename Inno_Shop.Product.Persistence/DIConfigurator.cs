using Inno_Shop.Product.Persistence.Helpers.UnitOfWork;
using Inno_Shop.Product.Persistence.Interfaces;
using Inno_Shop.Product.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Inno_Shop.Product.Persistence;

public static class DIConfigurator
{
    public static IServiceCollection ConfigurePersistence(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }

}