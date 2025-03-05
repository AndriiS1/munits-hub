using System.Security.Claims;
using MunitSHub.Domain.User;
namespace MunitSHub.UseCases.Services.Jwt;

public interface IJwtService
{
    string GenerateJsonWebToken(User user);
    (string refreshToken, DateTime refreshTokenExpiryTime) GenerateRefreshTokenData();
    IEnumerable<Claim>? GetPrincipalFromExpiredToken(string token);
}
