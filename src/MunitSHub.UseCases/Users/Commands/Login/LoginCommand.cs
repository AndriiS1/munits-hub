using MediatR;
using Microsoft.AspNetCore.Http;
namespace MunitSHub.UseCases.Users.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<IResult>;
