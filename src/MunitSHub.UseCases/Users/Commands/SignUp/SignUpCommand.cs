using MediatR;
using Microsoft.AspNetCore.Http;
namespace MunitSHub.UseCases.Users.Commands.SignUp;

public sealed record SignUpCommand : IRequest<IResult>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}