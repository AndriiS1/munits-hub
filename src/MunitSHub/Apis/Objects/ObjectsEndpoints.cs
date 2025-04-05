using MediatR;
using Microsoft.AspNetCore.Mvc;
using MunitSHub.Apis.Objects.Contracts;
namespace MunitSHub.Apis.Objects;

public static class ObjectsEndpoints
{
    private const string Source = "ObjectsApi";

    public static void MapObjectsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("objects/upload/initiate", async (HttpContext httpContext, [FromBody] InitiateMultipartUploadContract contract,
                [FromServices] IMediator mediator) => await mediator.Send(contract.ToCommand(httpContext.GetUserId())))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
        
        app.MapPost("objects/{uploadId}/abort", async (HttpContext httpContext, [FromBody] AbortMultipartUploadContract contract,
                [FromServices] IMediator mediator) => await mediator.Send(contract.ToCommand(httpContext.GetUserId())))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
        
        app.MapPost("objects/filter", async (HttpContext httpContext, [FromBody] GetObjectContract contract,
                [FromServices] IMediator mediator) => await mediator.Send(contract.ToQuery(httpContext.GetUserId())))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
    }
}
