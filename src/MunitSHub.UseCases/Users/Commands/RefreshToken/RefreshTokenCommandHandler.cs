using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MunitSHub.Domain.User;
using MunitSHub.UseCases.Services.Jwt;
namespace MunitSHub.UseCases.Users.Commands.RefreshToken;

public class RefreshTokenCommandHandler(IUserRepository userRepository, IJwtService jwtService) : IRequestHandler<RefreshTokenCommand, IResult>
{
    public async Task<IResult> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var claims = jwtService.GetPrincipalFromExpiredToken(command.AccessToken);

        if (claims == null)
            return Results.BadRequest("Invalid access or refresh token.");

        var userId = claims.Single(claim => claim.Type == JwtRegisteredClaimNames.NameId).Value;

        var user = await userRepository.Get(new ObjectId(userId));

        if (user == null)
        {
            return Results.NotFound("User is not found.");
        }

        if (user.RefreshToken != command.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return Results.BadRequest("Invalid access or refresh token.");
        }

        var newAccessToken = jwtService.GenerateJsonWebToken(user);
        var newRefreshTokenData = jwtService.GenerateRefreshTokenData();
        
        await userRepository.UpdateUserRefreshTokenData(user.Id, newRefreshTokenData.refreshToken, newRefreshTokenData.refreshTokenExpiryTime);
        
        return Results.Ok(new
        {
            accessToken = newAccessToken,
            newRefreshTokenData.refreshToken
        });
    }
}
