using Inno_Shop.Authentification.DTO;
using Inno_Shop.Authentification.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Authentification.Interfaces;

public interface IAuthRepository
{
    Task<ActionResult<UserDTO>> LoginAsync(LoginDTO loginDTO);
    Task<ActionResult<UserDTO>> RegisterAsync(RegisterDTO registerDTO);
    Task<ActionResult<UserDTO>> UpdateAsync(UserUpdateDto userDTO);
    Task<ActionResult> DeleteAsync(Guid userId);
}