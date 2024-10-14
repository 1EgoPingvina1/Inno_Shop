using Inno_Shop.Product.API.Extensions;
using Inno_Shop.Product.API.Middleware;
using Inno_Shop.Product.Application;
using Inno_Shop.Product.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddAuthenticationService(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.ConfigureApplication(builder.Configuration);
builder.Services.ConfigurePersistence();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseMiddleware<AuthorizationMiddleware>();
app.UseMiddleware<ExceptionHadlingMiddleware>();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();