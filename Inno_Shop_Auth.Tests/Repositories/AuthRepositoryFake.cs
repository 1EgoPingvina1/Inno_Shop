using Inno_Shop.Authentification.Application.Commands;
using Inno_Shop.Authentification.Domain.Models;
using Inno_Shop.Authentification.Domain.Services;
using Inno_Shop.Authentification.Infrastructure.Security;
using Inno_Shop.Authentification.Presentation.DTO;
using Inno_Shop.Authentification.Presentation.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Inno_Shop_Auth.Tests.Repositories;

public class AuthRepositoryFake
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<SignInManager<User>> _signInManagerMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly AuthRepository _authRepository;
    
    public AuthRepositoryFake()
    {
        var store = new Mock<IUserStore<User>>();
        _userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        _signInManagerMock = new Mock<SignInManager<User>>(
            _userManagerMock.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<User>>(),
            null, null, null, null);

        _tokenServiceMock = new Mock<ITokenService>();

        // Create the AuthRepository instance
        _authRepository = new AuthRepository(_userManagerMock.Object, _tokenServiceMock.Object, _signInManagerMock.Object);
    }
    
    [Fact]
    public async Task LoginAsync_ShouldReturnUserDTO_WhenCredentialsAreValid()
    {
        // Arrange
        var loginCommand = new LoginCommand
        {
            LoginDto = new LoginDTO("test@example.com", "Password123")
        };

        var user = new User
        {
            Email = loginCommand.LoginDto.Email,
            UserName = "testuser"
        };

        _userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        _signInManagerMock.Setup(sm => sm.CheckPasswordSignInAsync(user, loginCommand.LoginDto.Password, false))
            .ReturnsAsync(SignInResult.Success);

        _tokenServiceMock.Setup(ts => ts.CreateToken(It.IsAny<User>()))
            .Returns("token123");

        // Act
        var result = await _authRepository.LoginAsync(loginCommand);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(result.Email, loginCommand.LoginDto.Email);
        Assert.Equal("token123",result.Token);
    }
    
    [Fact]
    public async Task RegisterAsync_ShouldReturnUserDTO_WhenRegistrationIsSuccessful()
    {
        // Arrange
        var registerCommand = new RegisterCommand
        {
            Registerdto = new RegisterDTO { 
                Email = "register@example.com", 
                Password = "Password123",
                Username = "newuser"
            }
        };

        var user = new User
        {
            Email = registerCommand.Registerdto.Email,
            UserName = registerCommand.Registerdto.Username
        };

        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                        .ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), "Member"))
                        .ReturnsAsync(IdentityResult.Success);
        _tokenServiceMock.Setup(ts => ts.CreateToken(It.IsAny<User>()))
                         .Returns("token123");

        // Act
        var result = await _authRepository.RegisterAsync(registerCommand);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(result.Email,registerCommand.Registerdto.Email);
        Assert.Equal("token123",result.Token);
    }

    [Fact]
    public async Task ConfirmEmailAsync_ShouldThrowHttpException_WhenUserNotFound()
    {
        // Arrange
        var userId = "nonexistent-user";
        var token = "token123";
        _userManagerMock.Setup(um => um.FindByIdAsync(userId))
                        .ReturnsAsync((User)null);

        // Act
        var exeption = await Assert.ThrowsAsync<HttpExeption>(async () =>
            await _authRepository.ConfirmEmailAsync(userId, token)); 
        // Assert
        Assert.Equal("404 - User has not been found",exeption.Message);
    }

    [Fact]
    public async Task ForgotPasswordAsync_ShouldThrowHttpException_WhenUserNotFound()
    {
        // Arrange
        var command = new ForgotPasswordCommand { Email = "unknown@example.com" };
        _userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
                        .ReturnsAsync((User)null);

        // Act
        var exception = await Assert.ThrowsAsync<HttpExeption>(async () =>
            await _authRepository.ForgotPasswordAsync(command));

        // Assert
        Assert.Equal("404 - Email not found", exception.Message);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteUser_WhenUserExists()
    {
        // Arrange
        var userId = "existing-user";
        var user = new User { Id = userId };

        _userManagerMock.Setup(um => um.FindByIdAsync(userId))
                        .ReturnsAsync(user);
        _userManagerMock.Setup(um => um.DeleteAsync(It.IsAny<User>()))
                        .ReturnsAsync(IdentityResult.Success);

        // Act
        await _authRepository.DeleteAsync(userId);

        // Assert
        _userManagerMock.Verify(um => um.DeleteAsync(user), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnUserDTO_WhenUserIsUpdated()
    {
        // Arrange
        var updateCommand = new UpdateAccountCommand
        {
            Id = "user-id",
            Email = "updated@example.com",
            Username = "updateduser"
        };

        var user = new User
        {
            Id = updateCommand.Id,
            Email = "old@example.com",
            UserName = "olduser"
        };

        _userManagerMock.Setup(um => um.FindByIdAsync(updateCommand.Id))
                        .ReturnsAsync(user);

        // Act
        var result = await _authRepository.UpdateAsync(updateCommand);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(result.Email,updateCommand.Email);
        Assert.Equal(result.Username,updateCommand.Username);
    }
}