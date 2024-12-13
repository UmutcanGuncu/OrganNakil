using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrganNakil.Application.Abstractions.Token;
using OrganNakil.Application.Dtos.TokenDtos;

namespace OrganNakil.Persistence.Repositories;

public class TokenHandler : ITokenHandler
{
    IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Token CreateAccessToken(int minute)
    {
        Application.Dtos.TokenDtos.Token token = new();
        // Security Key'in simetriğini alıyoruz
        SymmetricSecurityKey securityKey = new (Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        //Şifrelenmiş Jimliği oluşturuyoruz
        SigningCredentials signingCredentials = new(securityKey,SecurityAlgorithms.HmacSha256);
        // Oluşturulacak token ayarlarını yap
        token.Expiration =DateTime.UtcNow.AddMinutes(minute);
        JwtSecurityToken securityToken = new(
            audience: _configuration["JWT:Audience"],
            issuer: _configuration["JWT:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow,//token üretildiği zamandan ne kadar süre sonra devreye girsin
            signingCredentials:signingCredentials
            );
        //Token oluşturucu sınıfından örnek alalım
        JwtSecurityTokenHandler handler = new ();
        token.AccessToken =handler.WriteToken(securityToken);
        token.RefreshToken = CreateRefreshAccessToken();
        return token;
    }

    public string CreateRefreshAccessToken()
    {
        byte[] number = new byte[32];
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes(number);
        return Convert.ToBase64String(number);
        
    }
}