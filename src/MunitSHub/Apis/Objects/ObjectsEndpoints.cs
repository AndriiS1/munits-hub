using MediatR;
using Microsoft.AspNetCore.Mvc;
using MunitSHub.Apis.Objects.Contracts;
using MunitSHub.UseCases.Objects.Queries.GetUploadSignedUrls;
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

        app.MapPost("objects/{objectId}/upload/{uploadId}/abort", async ([FromRoute] string objectId, [FromRoute] string uploadId, HttpContext httpContext,
                [FromBody] AbortMultipartUploadContract contract,
                [FromServices] IMediator mediator) => await mediator.Send(contract.ToCommand(httpContext.GetUserId(), objectId, uploadId)))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();

        app.MapPost("objects/{objectId}/upload/{uploadId}/complete", async ([FromRoute] string objectId ,[FromRoute] string uploadId, HttpContext httpContext,
                [FromBody] CompleteMultipartUploadContract contract,
                [FromServices] IMediator mediator) => await mediator.Send(contract.ToCommand(httpContext.GetUserId(), objectId, uploadId)))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();

        app.MapGet("objects/{objectId}/upload/{uploadId}/signed-urls", async ([FromRoute] string objectId, [FromRoute] string uploadId, HttpContext httpContext,
                [FromQuery] string bucketId, [FromQuery] long fileSize,
                [FromServices] IMediator mediator) => await mediator.Send(new GetUploadSignedUrlsQuery(httpContext.GetUserId(), bucketId, objectId, uploadId, fileSize)))
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
