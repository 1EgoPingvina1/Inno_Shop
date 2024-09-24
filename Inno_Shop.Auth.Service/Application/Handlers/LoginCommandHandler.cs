using System.Security.Authentication;
using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.Domain.Interfaces;
using Inno_Shop.Authentification.Presentation.DTO;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Authentification.Application.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand,ActionResult<UserDTO>>
{
    private IAuthRepository _AuthRepository;

    public LoginCommandHandler(IAuthRepository authRepository)
    {
        _AuthRepository = authRepository;
    }

    public async Task<ActionResult<UserDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        => await _AuthRepository.LoginAsync(request.LoginDto);
    
}