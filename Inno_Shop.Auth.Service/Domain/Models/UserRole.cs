using Microsoft.AspNetCore.Identity;

namespace Inno_Shop.Authentification.Domain.Models;

public class UserRole : IdentityUserRole<int>
{
    public User? User { get; set; }
    public Role? Role { get; set; }
}