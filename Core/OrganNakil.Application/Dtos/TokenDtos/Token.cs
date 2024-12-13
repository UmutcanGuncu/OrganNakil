namespace OrganNakil.Application.Dtos.TokenDtos;

public class Token
{
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; }
    public string RefreshToken { get; set; }
}