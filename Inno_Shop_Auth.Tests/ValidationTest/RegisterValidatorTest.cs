using FluentValidation.TestHelper;
using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.Application.Validators;
using Inno_Shop.Authentification.Presentation.DTO;

namespace Inno_Shop_Auth.Tests.ValidationTest;

public class RegisterValidatorTest
{
    
    private readonly RegisterValidator _validator;

    public RegisterValidatorTest()
    {
        _validator = new RegisterValidator();
    }
    
    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        // Arrange
        var command = new RegisterCommand
        {
            Registerdto = new RegisterDTO
            {
                Email = string.Empty,
                Username = "ValidUsername",
                Password = "ValidPassword123"
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Registerdto.Email)
              .WithErrorMessage("Email is required");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        // Arrange
        var command = new RegisterCommand
        {
            Registerdto = new RegisterDTO
            {
                Email ="invalid-email", 
                Username = "ValidUsername",
                Password = "ValidPassword123" 
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Registerdto.Email)
              .WithErrorMessage("Email is invalid");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Empty()
    {
        // Arrange
        var command = new RegisterCommand
        {
            Registerdto = new RegisterDTO
            {
                Email = "test@example.com",
                Username = "ValidUsername",
                Password = string.Empty
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Registerdto.Password)
              .WithErrorMessage("Password is required");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Too_Short()
    {
        // Arrange
        var command = new RegisterCommand
        {
            Registerdto = new RegisterDTO
            {
                Email = "test@example.com",
                Username = "ValidUsername",
                Password = "123"
            }        
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Registerdto.Password)
              .WithErrorMessage("Password must be at least 6 characters");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Too_Long()
    {
        // Arrange
        var command = new RegisterCommand
        {
            Registerdto = new RegisterDTO
            {
                Email = "test@example.com",
                Username = "ValidUsername",
                Password = new string('a',21)
            }        
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Registerdto.Password)
              .WithErrorMessage("Password must be between 6 and 20 characters");
    }

    [Fact]
    public void Should_Have_Error_When_Username_Is_Empty()
    {
        // Arrange
        var command = new RegisterCommand
        {
            Registerdto = new RegisterDTO
            {
                Email = "test@example.com",
                Username = string.Empty,
                Password = "string.Empty"
            }        
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Registerdto.Username)
              .WithErrorMessage("Username is required");
    }

    [Fact]
    public void Should_Have_Error_When_Username_Is_Too_Long()
    {
        // Arrange
        var command = new RegisterCommand
        {
            Registerdto = new RegisterDTO
            {
                Email = "test@example.com",
                Username = new string('a',102),
                Password = string.Empty
            }        
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Registerdto.Username)
              .WithErrorMessage("Max username length is 100 characters");
    }

    [Fact]
    public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
    {
        // Arrange
        var command = new RegisterCommand
        {
            Registerdto = new RegisterDTO
            {
                Email = "test@example.com",
                Username = "ValidUsername",
                Password = "Pa$$w0rd"
            }        
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.Registerdto.Email);
        result.ShouldNotHaveValidationErrorFor(x => x.Registerdto.Password);
        result.ShouldNotHaveValidationErrorFor(x => x.Registerdto.Username);
    }
}