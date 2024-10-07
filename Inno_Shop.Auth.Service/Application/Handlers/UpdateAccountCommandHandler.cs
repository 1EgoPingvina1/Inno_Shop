using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.Domain.Interfaces;
using Inno_Shop.Authentification.Presentation.DTO;
using MediatR;

namespace Inno_Shop.Authentification.Application.Handlers;

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand,UserDTO>
{
    private readonly IAuthRepository _authRepository;

    public UpdateAccountCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<UserDTO> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        => await _authRepository.UpdateAsync(request);
        
    
}