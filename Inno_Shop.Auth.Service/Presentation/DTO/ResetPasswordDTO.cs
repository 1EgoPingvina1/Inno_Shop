﻿namespace Inno_Shop.Authentification.DTO;

public class ResetPasswordDTO
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string Password { get; set; }
}