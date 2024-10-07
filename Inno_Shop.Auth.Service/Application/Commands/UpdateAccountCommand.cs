using Inno_Shop.Authentification.Presentation.DTO;
using MediatR;

namespace Inno_Shop.Authentification.Application.Commands;

public class UpdateAccountCommand : IRequest<UserDTO>
{
    public string Id { get; set; } = null!;
    public string? Username { get; set; }
    public string? Email { get; set; }
}