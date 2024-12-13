using OrganNakil.Application.Dtos.TokenDtos;
using OrganNakil.Domain.Entities;

namespace OrganNakil.Application.Interfaces;

public interface IUserRepository
{
    Task UpdateRefreshToken(string refreshToken,  AppUser user ,DateTime accessTokenExpiration, int addOnAccessTokenDate);
    Task<Token> RefreshTokenLoginAsync(string refreshToken);
}