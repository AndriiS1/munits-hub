using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MunitSHub.Domain.User;
using MunitSHub.UseCases.Services.HashService;
using MunitSHub.UseCases.Services.Jwt;
namespace MunitSHub.UseCases.Users.Commands.SignUp;

public class SignUpCommandHandler(IUserRepository userRepository, IJwtService jwtService, IHashService hashService) : IRequestHandler<SignUpCommand, IResult>
{
    public async Task<IResult> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        var refreshTokenDataDto = jwtService.GenerateRefreshTokenData();
        
        var user = new User
        {
            Id = ObjectId.GenerateNewId(),
            Email = command.Email,
            PasswordHash = hashService.GetHash(command.Password),
            RefreshToken = refreshTokenDataDto.refreshToken,
            RefreshTokenExpiryTime = refreshTokenDataDto.refreshTokenExpiryTime
        };

        var tryFindExistingUser = await userRepository.Get(command.Email);

        if (tryFindExistingUser != null)
            return Results.Conflict("User with this email already exists.");
        
        await userRepository.Create(user);

        var accessToken = jwtService.GenerateJsonWebToken(user);
        return Results.Ok(new
        {
            accessToken,
            refreshTokenDataDto.refreshToken
        });
    }
}
