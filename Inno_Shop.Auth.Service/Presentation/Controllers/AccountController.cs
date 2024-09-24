using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.Domain.Interfaces;
using Inno_Shop.Authentification.Domain.Models;
using Inno_Shop.Authentification.Presentation.DTO;
using Inno_Shop.Authentification.Presentation.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Authentification.Presentation.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IMediator _mediatr;
    private readonly IAuthRepository _authRepository;
    public AccountController(UserManager<User> userManager, IMediator mediatr, IAuthRepository authRepository)
    {
        _userManager = userManager;
        _mediatr = mediatr;
        _authRepository = authRepository;
    }
    
    [NonAction]
    public async Task<bool> CheckEmailExists([FromQuery] string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        => await _mediatr.Send(new LoginCommand() { LoginDto = loginDto });
    
    [HttpPost("Register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
        => await _mediatr.Send(new RegisterCommand() { Registerdto = registerDto });
    
    [Authorize]
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
        return await _authRepository.DeleteAsync(userId);
    }

    [Authorize]
    [HttpPut("ChangeAccountDetails")]
    public async Task<ActionResult<UserDTO>> UpdateUserDetails([FromBody] UserUpdateDto userDto)
        => await _authRepository.UpdateAsync(userDto);
}