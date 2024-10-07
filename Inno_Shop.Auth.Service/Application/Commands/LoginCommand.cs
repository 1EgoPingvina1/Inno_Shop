using Inno_Shop.Authentification.Presentation.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Authentification.Application.Commands;

public class LoginCommand : IRequest<UserDTO>
{
    public LoginDTO LoginDto { get; set; } = null!;
}