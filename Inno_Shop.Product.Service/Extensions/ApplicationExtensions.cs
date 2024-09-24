using System.Reflection;
using FluentValidation;
using Inno_Shop.Product.Service.Data;
using Inno_Shop.Product.Service.Helpers.AutoMapper;
using Inno_Shop.Product.Service.Helpers.UnitOfWork;
using Inno_Shop.Product.Service.Interfaces;
using Inno_Shop.Product.Service.Interfaces.Implementations;
using Inno_Shop.Product.Service.Middleware;
using Inno_Shop.Product.Service.Pipeline;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inno_Shop.Product.Service.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
    // Add database context with SQL Server connection
        services.AddDbContext<MainDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("MainConnection"));
        });

    // Register repository and unit of work for dependency injection
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IAuthChecker, AuthChecker>();

    // Register validation pipeline behavior for request validation
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineStep<,>));
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<HttpContext>(ctx => new DefaultHttpContext());
    // Add validators from the current assembly for automatic model validation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
    // Add MediatR for request handling and notification
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

    // Register exception handling middleware for error handling
        services.AddTransient<AuthorizationMiddleware>();
        services.AddTransient<ExceptionHadlingMiddleware>();
        return services;
    }
}