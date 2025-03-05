using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MunitSHub.Domain.User;
using MunitSHub.Infrastructure.Options.Jwt;
namespace MunitSHub.UseCases.Services.Jwt;

public class JwtService(IOptions<JwtOptions> options) : IJwtService
{
    public string GenerateJsonWebToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key));

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
        };

        var token = new JwtSecurityToken(
            options.Value.Issuer,
            options.Value.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(options.Value.TokenValidityInMinutes),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public (string refreshToken, DateTime refreshTokenExpiryTime) GenerateRefreshTokenData()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return (Convert.ToBase64String(randomNumber), DateTime.Now.AddMinutes(options.Value.TokenValidityInMinutes));
    }

    public IEnumerable<Claim>? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
        return jsonToken?.Claims;
    }
}
