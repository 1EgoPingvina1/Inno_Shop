using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Inno_Shop.Authentification.Data;
using Inno_Shop.Authentification.Data.Services;
using Inno_Shop.Authentification.Domain.Services;
using Inno_Shop.Authentification.DTO;
using Inno_Shop.Authentification.Infrastructure.Validation;
using Inno_Shop.Authentification.Interfaces;
using Inno_Shop.Authentification.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MediatR;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: Bearer 1safsfsdfdfd",
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddIdentityCore<User>(options => { })
    .AddRoles<Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddSignInManager<SignInManager<User>>()
    .AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Token:Key") ?? string.Empty)),
            ValidIssuer = builder.Configuration.GetValue<string>("Token:Issuer"),
            ValidateIssuer = true,
            ValidateAudience = false
        };
    });
builder.Services.AddScoped<IAuthRepository,AuthRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient<RegisterValidator>();

// builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterValidator>());
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

app.Run();