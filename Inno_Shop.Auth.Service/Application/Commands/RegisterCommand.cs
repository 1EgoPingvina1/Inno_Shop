using Inno_Shop.Authentification.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Authentification.Application.Commands;

public class RegisterCommand : IRequest<ActionResult<UserDTO>>
{
    public RegisterDTO Registerdto { get; set; } = null!;
}