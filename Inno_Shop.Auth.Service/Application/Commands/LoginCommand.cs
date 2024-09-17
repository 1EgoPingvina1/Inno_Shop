using Inno_Shop.Authentification.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Authentification.Application.Commands;

public class LoginCommand : IRequest<ActionResult<UserDTO>>
{
    public LoginDTO LoginDto { get; set; }
}