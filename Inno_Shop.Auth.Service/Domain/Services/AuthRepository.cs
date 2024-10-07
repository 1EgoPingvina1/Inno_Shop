using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.Domain.Interfaces;
using Inno_Shop.Authentification.Domain.Models;
using Inno_Shop.Authentification.Infrastructure.Security;
using Inno_Shop.Authentification.Presentation.DTO;
using Inno_Shop.Authentification.Presentation.Errors;
using Microsoft.AspNetCore.Identity;

namespace Inno_Shop.Authentification.Domain.Services;
public class AuthRepository : IAuthRepository
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<User> _signInManager;

    public AuthRepository(UserManager<User> userManager, 
        ITokenService tokenService, 
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    public async Task<UserDTO> LoginAsync(LoginCommand command)
    {
        var user = await _userManager.FindByEmailAsync(command.LoginDto.Email);

        if (user == null) 
            throw new HttpExeption(404,"Invalid login or password");

        var result = await _signInManager.CheckPasswordSignInAsync(user, command.LoginDto.Password, false);

        if (!result.Succeeded) 
            throw new HttpExeption(401,"Invalid login");
        
        return new UserDTO
        {
            Username= user.UserName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        };
    }

    public async Task<UserDTO> RegisterAsync(RegisterCommand command)
    {
        var user = new User()
        {
            UserName = command.Registerdto.Username,
            Email = command.Registerdto.Email, 
            EmailConfirmed = false
        };
        
        var result = await _userManager.CreateAsync(user, command.Registerdto.Password);
        await _userManager.AddToRoleAsync(user, "Member");
        if (!result.Succeeded)
            throw new HttpExeption(422, "Looks like the attempted has failed. Please check your data and try again.");

        return new UserDTO {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user),
            Email = user.Email
        };
    }

    public async Task<UserDTO> UpdateAsync(UpdateAccountCommand command)
    {
        var user = await _userManager.FindByIdAsync(command.Id);
        if (user is null)
            throw new HttpExeption(404,"User has not been found");
        user.UserName = command.Username;
        user.Email = command.Email;
        return new UserDTO
        {
            Username = user.UserName,
            Email = user.Email
        };    
    }

    public async Task ConfirmEmailAsync(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null) 
            throw new HttpExeption(404, "User has not been found");

        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (!result.Succeeded) 
            throw new HttpExeption(500, "Email confirmation failed");
    }

    public async Task ForgotPasswordAsync(ForgotPasswordCommand command)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);

        if (user == null) 
            throw new HttpExeption(404, "Email not found");

        await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task DeleteAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        await _userManager.DeleteAsync(user);
    }
}