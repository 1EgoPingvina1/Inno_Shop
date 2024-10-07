using Inno_Shop.Authentification.Presentation.DTO;
using MediatR;

namespace Inno_Shop.Authentification.Application.Commands;

public class ForgotPasswordCommand : IRequest<Unit>
{
    public string Email { get; set; } = null!;
}