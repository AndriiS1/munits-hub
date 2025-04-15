using MediatR;
using Microsoft.AspNetCore.Mvc;
using MunitSHub.Apis.Buckets.Contract;
using MunitSHub.UseCases.Buckets.Commands.Delete;
using MunitSHub.UseCases.Buckets.Queries.GetBucket;
using MunitSHub.UseCases.Buckets.Queries.GetBucketByName;
using HttpContext = Microsoft.AspNetCore.Http.HttpContext;
namespace MunitSHub.Apis.Buckets;

public static class BucketsEndpoints
{
    private const string Source = "BucketsApi";
    public static void MapBucketEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("buckets/", async (HttpContext httpContext, [FromBody] CreateBucketContract contract, [FromServices] IMediator mediator) =>
            await mediator.Send(contract.ToCommand(httpContext.GetUserId())))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();

        app.MapPost("buckets/exists", async ([FromBody] BucketExistsContract contract, [FromServices] IMediator mediator) =>
            await mediator.Send(contract.ToQuery()))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();

        app.MapDelete("buckets/", async ([FromBody] DeleteBucketCommand command, [FromServices] IMediator mediator) => await mediator.Send(command))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();

        app.MapGet("buckets/{id}", async (string id, HttpContext httpContext, [FromServices] IMediator mediator) =>
            await mediator.Send(new GetBucketQuery(httpContext.GetUserId(), id)))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
        
        app.MapGet("buckets/by-name/{bucketName}", async (string bucketName, HttpContext httpContext, [FromServices] IMediator mediator) =>
            await mediator.Send(new GetBucketByNameQuery(httpContext.GetUserId(), bucketName)))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();

        app.MapPost("buckets/filter", async (HttpContext httpContext, [FromBody] GetBucketsContract contract, [FromServices] IMediator mediator) =>
            await mediator.Send(contract.ToCommand(httpContext.GetUserId())))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
        
        app.MapPost("buckets/search", async (HttpContext httpContext, [FromBody] SearchBucketsContract contract, [FromServices] IMediator mediator) =>
            await mediator.Send(contract.ToCommand(httpContext.GetUserId())))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
    }
}
