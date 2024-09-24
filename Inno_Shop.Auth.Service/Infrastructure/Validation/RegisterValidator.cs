using FluentValidation;
using Inno_Shop.Authentification.Domain.Interfaces;
using Inno_Shop.Authentification.Presentation.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Authentification.Infrastructure.Validation;

/// <summary>
/// Validator for RegisterDTO objects
/// </summary>
public class RegisterValidator : AbstractValidator<RegisterDTO>
{
    private readonly IEmailSender _emailSender;

    /// <summary>
    /// Constructor for RegisterValidator
    /// </summary>
    /// <param name="emailSender">Email sender interface</param>
    public RegisterValidator(IEmailSender emailSender)
    {
        _emailSender = emailSender;

        /// <summary>
        /// Validation rules for Username
        /// </summary>
        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters")
            .MaximumLength(20).WithMessage("Username must not exceed 20 characters")
            .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Username can only contain letters, numbers, and underscores");

        /// <summary>
        /// Validation rules for Email
        /// </summary>
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address")
            .MustAsync(UniqueEmail).WithMessage("Email address is already in use")
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("Invalid email domain");

        /// <summary>
        /// Validation rules for Password
        /// </summary>
        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .MaximumLength(20).WithMessage("Password must not exceed 20 characters")
            .Must(HasUppercase).WithMessage("Password must have at least one uppercase letter")
            .Must(HasLowercase).WithMessage("Password must have at least one lowercase letter")
            .Must(HasNumber).WithMessage("Password must have at least one number")
            .Must(HasNonAlphanumeric).WithMessage("Password must have at least one non-alphanumeric character");
    }

    /// <summary>
    /// Checks if an email address is unique
    /// </summary>
    /// <param name="model">RegisterDTO object</param>
    /// <param name="email">Email address to check</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the email address is unique, false otherwise</returns>
    private async Task<bool> UniqueEmail(RegisterDTO model, string email, CancellationToken cancellationToken) 
        => !await _emailSender.CheckEmailExists(email);
    
        private bool HasUppercase(string password) => password.Any(c => char.IsUpper(c));
        private bool HasLowercase(string password) => password.Any(c => char.IsLower(c));
        private bool HasNumber(string password) => password.Any(c => char.IsDigit(c));
        private bool HasNonAlphanumeric(string password) => password.Any(c => !char.IsLetterOrDigit(c));
}