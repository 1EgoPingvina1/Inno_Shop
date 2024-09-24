using Inno_Shop.Authentification.Presentation.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Authentification.Domain.Interfaces;

public interface IAuthRepository
{
    Task<ActionResult<UserDTO>> LoginAsync(LoginDTO loginDto);
    Task<ActionResult<UserDTO>> RegisterAsync(RegisterDTO registerDto);
    Task<ActionResult<UserDTO>> UpdateAsync(UserUpdateDto userDto);
    Task<ActionResult> DeleteAsync(Guid userId);
}