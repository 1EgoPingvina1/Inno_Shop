using Inno_Shop.Product.Service.Data;
using Inno_Shop.Product.Service.Helpers.UnitOfWork;
using Inno_Shop.Product.Service.Helpers.Validation;
using Inno_Shop.Product.Service.Interfaces;
using Inno_Shop.Product.Service.Interfaces.Implementations;
using Inno_Shop.Product.Service.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MainDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MainConnection"));
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ProductValidator>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();