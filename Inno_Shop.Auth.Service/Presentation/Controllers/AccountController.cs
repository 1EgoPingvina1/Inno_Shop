using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.Domain.Interfaces;
using Inno_Shop.Authentification.Presentation.DTO;
using Inno_Shop.Authentification.Presentation.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Inno_Shop.Authentification.Presentation.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AccountController : Controller
{
    private readonly IMediator _mediatr;
    private readonly IAuthRepository _authRepository;
    public AccountController(IMediator mediatr, IAuthRepository authRepository)
    {
        _mediatr = mediatr;
        _authRepository = authRepository;
    }
    
    [Authorize]
    [HttpGet("ConfirmEmail")]
    public async Task<ActionResult> ConfirmEmail([FromQuery] string userId, string token)
    {
        await _authRepository.ConfirmEmailAsync(userId, token);
        return Ok("Email has been confirmed");
    }
    
    [HttpPost("Login")]
    [SwaggerOperation("Sign in user in the system")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        => await _mediatr.Send(new LoginCommand() { LoginDto = loginDto });
    
    [AllowAnonymous]
    [HttpPost("Register")]
    [SwaggerOperation("Sign up user in the system")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
        => await _mediatr.Send(new RegisterCommand() { Registerdto = registerDto });
    
    [AllowAnonymous]
    [HttpPost("ForgotPassword")]
    [SwaggerOperation("Operation for reseting password with sending a code to email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ForgotPassword(ForgotPasswordCommand command)
    {
        await _authRepository.ForgotPasswordAsync(command);
        return Ok("Your password has been sent to your email address");
    }
    
    [Authorize]
    [HttpDelete("DeleteAccount/{userId}")]
    [SwaggerOperation("Operation for deleting accounts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> DeleteUser( string userId)
        => Ok(await _mediatr.Send(new DeleteAccountCommand() { UserId = userId }));

    [Authorize]
    [HttpPut("ChangeAccountDetails")]
    public async Task<ActionResult<UserDTO>> UpdateUserDetails([FromBody] UpdateAccountCommand userDto)
        => Ok(await _authRepository.UpdateAsync(userDto));
}