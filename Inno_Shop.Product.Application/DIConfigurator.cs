using System.Reflection;
using Inno_Shop.Product.Application.AutoMapper;
using Inno_Shop.Product.Application.CQRS.Command;
using Inno_Shop.Product.Application.CQRS.Handler.Command;
using Inno_Shop.Product.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inno_Shop.Product.Application;

public static class DIConfigurator
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddTransient<IRequestHandler<CreateProductCommand, CreateProductCommand>, CreateProductCommandHandler>();
        services.AddDbContext<MainDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("MainConnection"));
        }); 
        services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(DIConfigurator).Assembly));
        
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        return services;
    }
}