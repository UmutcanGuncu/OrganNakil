using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrganNakil.Application.Abstractions.Token;
using OrganNakil.Application.Dtos.TokenDtos;
using OrganNakil.Application.Interfaces;
using OrganNakil.Domain.Entities;
using OrganNakil.Persistence.Context;

namespace OrganNakil.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenHandler _tokenHandler;
    public UserRepository(UserManager<AppUser> userManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
    }

    public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenExpiration, int addOnAccessTokenDate)
    {
       if (user != null)
       {
           user.RefreshToken = refreshToken;
           user.RefreshTokenExpiry = accessTokenExpiration.AddMinutes(addOnAccessTokenDate);
           await _userManager.UpdateAsync(user);
       }
      
    }

    public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
    {
        AppUser? user = await _userManager.Users.FirstOrDefaultAsync(user => user.RefreshToken == refreshToken);
        if (user != null && user.RefreshTokenExpiry > DateTime.UtcNow)
        {
            Token token = _tokenHandler.CreateAccessToken(5);
            await UpdateRefreshToken(token.RefreshToken, user,token.Expiration,2);
            return token;
        }
        throw new UnauthorizedAccessException();
        
       
    }
}