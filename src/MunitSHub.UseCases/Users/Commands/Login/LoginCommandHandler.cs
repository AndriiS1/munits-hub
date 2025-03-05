using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.User;
using MunitSHub.UseCases.Services.HashService;
using MunitSHub.UseCases.Services.Jwt;
namespace MunitSHub.UseCases.Users.Commands.Login;

public class LoginCommandHandler(IUserRepository userRepository, IJwtService jwtService, IHashService hashService) : IRequestHandler<LoginCommand, IResult>
{
    public async Task<IResult> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.Get(command.Email, hashService.GetHash(command.Password));

        if (user == null)
            return Results.NotFound("User is not found.");

        var accessToken = jwtService.GenerateJsonWebToken(user);
        var refreshTokenData = jwtService.GenerateRefreshTokenData();

        await userRepository.UpdateUserRefreshTokenData(user.Id, refreshTokenData.refreshToken, refreshTokenData.refreshTokenExpiryTime);

        return Results.Ok(new
        {
            accessToken, 
            refreshTokenData.refreshToken
        });
    }
}
