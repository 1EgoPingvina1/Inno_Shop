using FluentValidation;
using Inno_Shop.Authentification.Application.Commands;

namespace Inno_Shop.Authentification.Application.Validators;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(r => r.Registerdto.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is invalid");
        
        RuleFor(r => r.Registerdto.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .MaximumLength(20).WithMessage("Password must be between 6 and 20 characters");

        RuleFor(r => r.Registerdto.Username)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(100).WithMessage("Max username length is 100 characters");
    }
}