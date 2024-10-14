using System.Reflection;
using FluentValidation;
using Inno_Shop.Product.API.Middleware;
using Inno_Shop.Product.API.Pipeline;
using MediatR;

namespace Inno_Shop.Product.API.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        
    // Register validation pipeline behavior for request validation
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineStep<,>));
        services.AddHttpContextAccessor();
    // Add validators from the current assembly for automatic model validation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    // Add MediatR for request handling and notification
        // services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Program).Assembly));

    // Register exception handling middleware for error handling
        services.AddTransient<AuthorizationMiddleware>();
        services.AddTransient<ExceptionHadlingMiddleware>();
        return services;
    }
}