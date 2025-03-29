using MediatR;
using Microsoft.AspNetCore.Mvc;
using MunitSHub.Apis.Objects.Contract;
namespace MunitSHub.Apis.Objects;

public static class ObjectsEndpoints
{
    private const string Source = "ObjectsApi";

    public static void MapObjectsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("buckets/filter", async (HttpContext httpContext, [FromBody] GetObjectContract contract,
                [FromServices] IMediator mediator) => await mediator.Send(contract.ToQuery(httpContext.GetUserId())))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
    }
}
