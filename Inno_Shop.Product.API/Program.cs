using System.Text;
using Inno_Shop.Product.API.Extensions;
using Inno_Shop.Product.API.Middleware;
using Inno_Shop.Product.Application;
using Inno_Shop.Product.Persistence;
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
builder.Services.AddApplicationServices();

//Configuration from other projects
builder.Services.ConfigureApplication(builder.Configuration);
builder.Services.ConfigurePersistence();

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