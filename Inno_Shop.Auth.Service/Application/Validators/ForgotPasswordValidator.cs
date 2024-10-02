using FluentValidation;
using Inno_Shop.Authentification.Application.Commands;

namespace Inno_Shop.Authentification.Application.Validators;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordValidator()
    {
        RuleFor(f => f.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256)
            .WithMessage("Please enter a valid email address.");
    }
}