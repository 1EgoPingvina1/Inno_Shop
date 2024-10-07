using System.Runtime.InteropServices.JavaScript;
using Azure.Core;
using FluentValidation;
using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.Domain.Interfaces;
using Inno_Shop.Authentification.Presentation.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Authentification.Application.Handlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserDTO>
{
    private readonly IAuthRepository _authRepository;

    public RegisterCommandHandler(IAuthRepository authRepository) 
    {
        _authRepository = authRepository;
    }
    public async Task<UserDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
        => await _authRepository.RegisterAsync(request);
    
}