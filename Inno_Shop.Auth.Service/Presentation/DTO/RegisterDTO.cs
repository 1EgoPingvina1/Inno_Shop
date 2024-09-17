using System.ComponentModel.DataAnnotations;

namespace Inno_Shop.Authentification.DTO;

public class RegisterDTO
{
    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}