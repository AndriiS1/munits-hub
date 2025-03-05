using MediatR;
using Microsoft.AspNetCore.Mvc;
using MunitSHub.UseCases.Users.Commands.Login;
using MunitSHub.UseCases.Users.Commands.RefreshToken;
using MunitSHub.UseCases.Users.Commands.SignUp;
namespace MunitSHub.Apis.User;

public static class UserEndpoints
{
    private const string Source = "UsersApi";
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async ([FromBody] LoginCommand command, [FromServices] IMediator mediator) => await mediator.Send(command))
            .WithGroupName(Source)
            .DisableAntiforgery();
        
        app.MapPost("users/sign-up", async ([FromBody] SignUpCommand command, [FromServices] IMediator mediator) => await mediator.Send(command))
            .WithGroupName(Source)
            .DisableAntiforgery();
        
        app.MapPost("users/refresh-token", async ([FromBody] RefreshTokenCommand command, [FromServices] IMediator mediator) => await mediator.Send(command))
            .WithGroupName(Source)
            .DisableAntiforgery();
    }
}
