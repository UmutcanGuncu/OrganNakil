namespace OrganNakil.Application.Abstractions.Token;

public interface ITokenHandler
{
    Dtos.TokenDtos.Token CreateAccessToken(int minute); 
    string CreateRefreshAccessToken();
}