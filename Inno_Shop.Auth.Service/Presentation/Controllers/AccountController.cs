using AutoMapper;
using Inno_Shop.Authentification.API.Errors;
using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.Data.Services;
using Inno_Shop.Authentification.DTO;
using Inno_Shop.Authentification.Interfaces;
using Inno_Shop.Authentification.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Authentification.Controllers;

public class AccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly RoleManager<Role> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;
    private IMediator _mediatr;
    private IAuthRepository _AuthRepository;

    public AccountController(
        ITokenService tokenService,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IMapper mapper, RoleManager<Role> roleManager, IMediator mediatr, IAuthRepository authRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _tokenService = tokenService;
        _roleManager = roleManager;
        _mediatr = mediatr;
        _AuthRepository = authRepository;
    }

    public async Task<bool> CheckEmailExists([FromQuery] string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        => await _mediatr.Send(new LoginCommand() { LoginDto = loginDto });
    
    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
        => await _mediatr.Send(new RegisterCommand() { Registerdto = registerDto });
    
    [HttpGet("ConfirmEmail")]
    public async Task<ActionResult<string>> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) 
            return NotFound(new HttpExeption(404, "User has not been found"));

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded) 
            return BadRequest(new HttpExeption(400, result.Errors.ToString()));

        return "Email confirmed successfully";
    }
    
    [HttpPost("ForgotPassword")]
    public async Task<ActionResult<string>> ForgotPassword(ForgotPasswordDTO forgotPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

        if (user == null) 
            return NotFound(new HttpExeption(404, "Email not found"));

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Send the token to the user's email
        // ...

        return "Password reset token sent successfully";
    }

    [HttpPost("ResetPassword")]
    public async Task<ActionResult<string>> ResetPassword(ResetPasswordDTO resetPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

        if (user == null) 
            return NotFound(new HttpExeption(404,"User is not found"));

        var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);

        if (!result.Succeeded) 
            return BadRequest(new HttpExeption(400,"Sending token has failed, try again latter"));

        return "Password reset successfully";
    }

    [Authorize]
    [HttpDelete("DeleteAccount")]
    public async Task<ActionResult> DeleteUser([FromQuery] Guid userId)
    {
        return await _AuthRepository.DeleteAsync(userId);
    }

    [Authorize]
    [HttpPut("ChangeAccountDetails")]
    public async Task<ActionResult<UserDTO>> UpdateUserDetails([FromBody] UserUpdateDto userDto)
        => await _AuthRepository.UpdateAsync(userDto);
}