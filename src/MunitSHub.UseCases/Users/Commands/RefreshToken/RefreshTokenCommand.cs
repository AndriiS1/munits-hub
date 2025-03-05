using MediatR;
using Microsoft.AspNetCore.Http;
namespace MunitSHub.UseCases.Users.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<IResult>;