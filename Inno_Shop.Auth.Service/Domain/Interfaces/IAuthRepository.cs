using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.Domain.Models;
using Inno_Shop.Authentification.Presentation.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Authentification.Domain.Interfaces;

public interface IAuthRepository
{
    Task<UserDTO> LoginAsync(LoginCommand command);
    Task<UserDTO> RegisterAsync(RegisterCommand command);
    Task<UserDTO> UpdateAsync(UpdateAccountCommand command);
    Task ConfirmEmailAsync(string userId, string code);
    Task ForgotPasswordAsync(ForgotPasswordCommand command);
    Task DeleteAsync(string userId);
}