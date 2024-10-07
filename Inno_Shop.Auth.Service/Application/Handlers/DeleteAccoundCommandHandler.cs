using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.Domain.Interfaces;
using Inno_Shop.Authentification.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Inno_Shop.Authentification.Application.Handlers;

public class DeleteAccoundCommandHandler : IRequestHandler<DeleteAccountCommand,bool>
{
    private readonly IAuthRepository _authRepository;
    private readonly UserManager<User> _userManager;

    public DeleteAccoundCommandHandler(IAuthRepository authRepository, UserManager<User> userManager)
    {
        _authRepository = authRepository;
        _userManager = userManager;
    }

    public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if(user is null)
            return false;
        
        await _authRepository.DeleteAsync(user.Id.ToString());
        return true;
    }
}