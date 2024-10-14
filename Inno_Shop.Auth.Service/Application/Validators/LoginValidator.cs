using FluentValidation;
using Inno_Shop.Authentification.Application.Commands;

namespace Inno_Shop.Authentification.Application.Validators;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.LoginDto.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email address")
            .MaximumLength(256).WithMessage("Email address is too long");

        RuleFor(x => x.LoginDto.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"\d").WithMessage("Password must contain at least one digit")
            .Matches(@"[!@#$%^&*()_+=-{};:'<>,./?]").WithMessage("Password must contain at least one special character");
    }

}