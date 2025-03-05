using MediatR;
using Microsoft.AspNetCore.Mvc;
using MunitSHub.UseCases.Buckets.Commands.Create;
using MunitSHub.UseCases.Buckets.Commands.Delete;
using MunitSHub.UseCases.Buckets.Queries.GetBucket;
using MunitSHub.UseCases.Buckets.Queries.GetBuckets;
namespace MunitSHub.Apis.Buckets;

public static class BucketEndpoints
{
    private const string Source = "BucketsApi";
    public static void MapBucketEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("buckets/", async ([FromBody] CreateBucketCommand command, [FromServices] IMediator mediator) => await mediator.Send(command))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
        
        app.MapDelete("buckets/", async ([FromBody] DeleteBucketCommand command, [FromServices] IMediator mediator) => await mediator.Send(command))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
        
        app.MapGet("buckets/{{name}}", async (string name, [FromServices] IMediator mediator) => await mediator.Send(new GetBucketCommand(name)))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
        
        app.MapPost("buckets/filter", async ([FromBody] GetBucketsCommand command, [FromServices] IMediator mediator) => await mediator.Send(command))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
    }
}
