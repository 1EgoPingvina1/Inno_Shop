using Inno_Shop.Authentification.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Inno_Shop.Authentification.Application.Extensions;

public class EmailTokenProvider<TUser > : IUserTwoFactorTokenProvider<TUser> where TUser  : User
{
    public async Task<string> GenerateAsync(string purpose, UserManager<TUser > manager, TUser  user)
    {
        return await Task.FromResult("your_token_here");
    }

    public async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser > manager, TUser  user)
    {
        return await Task.FromResult(true);
    }

    public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
    {
        throw new NotImplementedException();
    }
}