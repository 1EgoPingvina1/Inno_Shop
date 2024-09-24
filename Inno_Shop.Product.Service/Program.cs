using System.Text;
using Inno_Shop.Product.Service.Extensions;
using Inno_Shop.Product.Service.Interfaces;
using Inno_Shop.Product.Service.Interfaces.Implementations;
using Inno_Shop.Product.Service.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddAuthenticationService(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<AuthorizationMiddleware>();
app.UseMiddleware<ExceptionHadlingMiddleware>();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();