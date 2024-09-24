using Inno_Shop.Authentification.Domain.Interfaces;
using Inno_Shop.Authentification.Domain.Models;
using Inno_Shop.Authentification.Infrastructure.Security;
using Inno_Shop.Authentification.Infrastructure.Validation;
using Inno_Shop.Authentification.Presentation.DTO;
using Inno_Shop.Authentification.Presentation.Errors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Authentification.Domain.Services;
public class AuthRepository : IAuthRepository
{
    private readonly UserManager<User> _userManager;
    private readonly RegisterValidator _registerValidator;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<User> _signInManager;

    public AuthRepository(UserManager<User> userManager, RegisterValidator registerValidator, ITokenService tokenService, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _registerValidator = registerValidator;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    public async Task<ActionResult<UserDTO>> LoginAsync(LoginDTO loginDTO)
    {
        var user = await _userManager.FindByEmailAsync(loginDTO.Email);

        if (user == null) 
            throw new ArgumentException("Invalid login or password");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

        if (!result.Succeeded) 
            throw new HttpExeption(403,"Invalid login");
        
        return new UserDTO
        {
            Username= user.UserName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        };
    }

    public async Task<ActionResult<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
    {
        var validator = await _registerValidator.ValidateAsync(registerDTO);
        if (!validator.IsValid)
            throw new HttpExeption(409, "Check your data before trying to register.");
        
        var user = new User()
        {
            UserName = registerDTO.Username,
            Email = registerDTO.Email, EmailConfirmed = false
        };
        
        var result = await _userManager.CreateAsync(user, registerDTO.Password);
        await _userManager.AddToRoleAsync(user, "Member");
        if (!result.Succeeded)
            throw new HttpExeption(500, "Looks like the attempted has failed. Try again later.");

        return new UserDTO
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user),
            Email = user.Email
        };
    }

    public async Task<ActionResult<UserDTO>> UpdateAsync(UserUpdateDto userDTO)
    {
        // var user = await _userManager.GetUserAsync(User.Identity.Name);
        // if (user is not null)
        // {
        //     user.PhoneNumber = userDTO.PhoneNumber;
        //     await _userManager.UpdateAsync(user);
        //     return new UserDTO(user));
        // }
        
        throw new HttpExeption(404, "User has not been found");
    }

    public async Task<ActionResult> DeleteAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is not null)
            await _userManager.DeleteAsync(user);
        
        throw new HttpExeption(404,"User has not been found");
    }
}