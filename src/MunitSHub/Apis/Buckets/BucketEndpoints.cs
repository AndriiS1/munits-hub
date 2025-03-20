using MediatR;
using Microsoft.AspNetCore.Mvc;
using MunitSHub.Apis.Buckets.Contract;
using MunitSHub.UseCases.Buckets.Commands.Delete;
using MunitSHub.UseCases.Buckets.Queries.GetBucket;
namespace MunitSHub.Apis.Buckets;

public static class BucketEndpoints
{
    private const string Source = "BucketsApi";
    public static void MapBucketEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("buckets/", async (HttpContext httpContext, [FromBody] CreateBucketContract contract, [FromServices] IMediator mediator) => 
                await mediator.Send(contract.ToCommand(httpContext.GetUserId())))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
        
        app.MapDelete("buckets/", async ([FromBody] DeleteBucketCommand command, [FromServices] IMediator mediator) => await mediator.Send(command))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
        
        app.MapGet("buckets/{{id}}", async (string id, HttpContext httpContext, [FromServices] IMediator mediator) =>
            await mediator.Send(new GetBucketCommand(httpContext.GetUserId(), id)))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
        
        app.MapPost("buckets/user", async (HttpContext httpContext, [FromBody] GetBucketsContract contract, [FromServices] IMediator mediator) =>
                await mediator.Send(contract.ToCommand(httpContext.GetUserId())))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
    }
}
