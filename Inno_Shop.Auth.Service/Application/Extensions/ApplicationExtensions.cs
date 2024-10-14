using System.Reflection;
using FluentValidation;
using Inno_Shop.Authentification.Application.Middleware;
using Inno_Shop.Authentification.Domain.Interfaces;
using Inno_Shop.Authentification.Domain.Services;
using Inno_Shop.Authentification.Infrastructure.FluentPipline;
using Inno_Shop.Authentification.Infrastructure.Security;
using MediatR;

namespace Inno_Shop.Authentification.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthRepository,AuthRepository>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(typeof(Program));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient<IForgetPasswordService, ForgetPasswordService>();
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddCors(opt =>
        {
            opt.AddPolicy("AllowAll", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        services.AddTransient<ExceptionMiddleware>();
        return services;
    }
}