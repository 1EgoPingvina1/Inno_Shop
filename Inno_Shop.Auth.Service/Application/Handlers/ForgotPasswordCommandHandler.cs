using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Inno_Shop.Authentification.Application.Handlers;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand,Unit>
{
    private UserManager<User> _userMandager;

    public ForgotPasswordCommandHandler(UserManager<User> userMandager)
    {
        _userMandager = userMandager;
    }

    public Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        // var user =await _userMandager.FindByEmailAsync(request.Email);
        // if (user is null || !await _userMandager.IsEmailConfirmedAsync(user))
        // {
        //     return 
        // }
            throw new System.NotImplementedException();
        
    }
}