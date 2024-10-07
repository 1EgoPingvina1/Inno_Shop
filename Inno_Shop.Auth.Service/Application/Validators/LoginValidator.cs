using FluentValidation;
using Inno_Shop.Authentification.Application.Commands;

namespace Inno_Shop.Authentification.Application.Validators;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.LoginDto.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email address");

        RuleFor(x => x.LoginDto.Password)
            .NotEmpty().WithMessage("Password is required");
    }

}