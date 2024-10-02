using System.Runtime.InteropServices.JavaScript;
using Azure.Core;
using FluentValidation;
using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.Domain.Interfaces;
using Inno_Shop.Authentification.Presentation.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Authentification.Application.Handlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ActionResult<UserDTO>>
{
    private IAuthRepository _AuthRepository;

    public RegisterCommandHandler(IAuthRepository authRepository) 
    {
        _AuthRepository = authRepository;
    }
    public async Task<ActionResult<UserDTO>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _AuthRepository.RegisterAsync(request.Registerdto);
        // var validator = await _registerValidator.ValidateAsync(request.Registerdto, cancellationToken);
        // if (!validator.IsValid)
        //     throw new HttpExeption(422, validator.Errors.ToString());
        //
        // var user = new User() {
        //     UserName = request.Registerdto.Username,
        //     Email = request.Registerdto.Email,
        //     EmailConfirmed = false
        // };
        //
        // var result = await _userManager.CreateAsync(user,request.Registerdto.Password);
        // await _userManager.AddToRoleAsync(user, "Member");
        // if (!result.Succeeded) 
        //     throw new HttpExeption(422, validator.Errors.ToString());
        //
        // return new UserDTO {
        //     Username= user.UserName,
        //     Token = _tokenService.CreateToken(user),
        //     Email = user.Email
        // };
    }
}