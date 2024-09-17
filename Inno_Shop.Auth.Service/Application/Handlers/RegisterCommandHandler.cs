using System.Runtime.InteropServices.JavaScript;
using Azure.Core;
using FluentValidation;
using Inno_Shop.Authentification.API.Errors;
using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.DTO;
using Inno_Shop.Authentification.Infrastructure.Validation;
using Inno_Shop.Authentification.Interfaces;
using Inno_Shop.Authentification.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Authentification.Application.Handlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ActionResult<UserDTO>>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly RegisterValidator _registerValidator;
    private IAuthRepository _AuthRepository;

    public RegisterCommandHandler(ITokenService tokenService, 
        UserManager<User> userManager, 
        RegisterValidator registerValidator, 
        IServiceProvider serviceProvider, IAuthRepository authRepository) 
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _registerValidator = registerValidator;
        _serviceProvider = serviceProvider;
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