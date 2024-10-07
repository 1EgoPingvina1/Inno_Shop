using System.Reflection;
using System.Text;
using FluentValidation;
using Inno_Shop.Authentification.Application.Extensions;
using Inno_Shop.Authentification.Application.Middleware;
using Inno_Shop.Authentification.Domain.Interfaces;
using Inno_Shop.Authentification.Domain.Models;
using Inno_Shop.Authentification.Domain.Services;
using Inno_Shop.Authentification.Infrastructure.Data;
using Inno_Shop.Authentification.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.InjectIdentityServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();