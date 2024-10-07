using FluentValidation.TestHelper;
using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.Application.Validators;
using Inno_Shop.Authentification.Presentation.DTO;

namespace Inno_Shop_Auth.Tests.ValidationTest;

public class LoginValidatorTest
{
    private readonly LoginValidator _validator;

    public LoginValidatorTest()
    {
        _validator = new LoginValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        // Arrange
        var command = new LoginCommand
        {
            LoginDto = new LoginDTO("","")
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.LoginDto.Email)
            .WithErrorMessage("Email is required");
    }
    
    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        // Arrange
        var command = new LoginCommand
        {
            LoginDto = new LoginDTO("invalid-email", "SomePassword123")
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.LoginDto.Email)
            .WithErrorMessage("Invalid Email address");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Empty()
    {
        // Arrange
        var command = new LoginCommand
        {
            LoginDto = new LoginDTO("test@example.com", "")
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.LoginDto.Password)
            .WithErrorMessage("Password is required");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Email_And_Password_Are_Valid()
    {
        // Arrange
        var command = new LoginCommand
        {
            LoginDto = new LoginDTO("valid@example.com", "ValidPassword123")
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.LoginDto.Email);
        result.ShouldNotHaveValidationErrorFor(x => x.LoginDto.Password);
    }
}