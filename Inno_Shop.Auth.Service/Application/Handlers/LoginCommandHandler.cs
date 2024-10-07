using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.Domain.Interfaces;
using Inno_Shop.Authentification.Presentation.DTO;
using MediatR;

namespace Inno_Shop.Authentification.Application.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand,UserDTO>
{
    private readonly IAuthRepository _authRepository;
    public LoginCommandHandler(IAuthRepository authRepository) => _authRepository = authRepository;

    public async Task<UserDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _authRepository.LoginAsync(request);
        return user;
    }
}