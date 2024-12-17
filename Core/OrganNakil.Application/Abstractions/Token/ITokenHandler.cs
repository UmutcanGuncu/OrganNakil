using OrganNakil.Domain.Entities;

namespace OrganNakil.Application.Abstractions.Token;

public interface ITokenHandler
{
    Dtos.TokenDtos.Token CreateAccessToken(int minute, AppUser user); 
    string CreateRefreshAccessToken();
}