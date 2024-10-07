using MediatR;

namespace Inno_Shop.Authentification.Application.Commands;

public class DeleteAccountCommand : IRequest<bool>
{
    public string UserId { get; set; } = null!;
}