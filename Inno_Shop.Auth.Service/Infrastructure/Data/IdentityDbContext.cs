using Inno_Shop.Authentification.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inno_Shop.Authentification.Data;

public class IdentityContext : IdentityDbContext<User, Role, string>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}