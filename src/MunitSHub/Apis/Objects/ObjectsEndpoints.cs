using MediatR;
using Microsoft.AspNetCore.Mvc;
using MunitSHub.Apis.Objects.Contracts;
using MunitSHub.UseCases.Objects.Queries.GetObjectQuery;
using MunitSHub.UseCases.Objects.Queries.GetUploadSignedUrls;
namespace MunitSHub.Apis.Objects;

public static class ObjectsEndpoints
{
    private const string Source = "ObjectsApi";

    public static void MapObjectsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("buckets/{bucketId}/objects/uploads/initiate", async (HttpContext httpContext, [FromRoute] string bucketId, [FromBody] InitiateMultipartUploadContract contract,
                [FromServices] IMediator mediator) => await mediator.Send(contract.ToCommand(httpContext.GetUserId(), bucketId)))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();

        app.MapPost("buckets/{bucketId}/objects/{objectId}/uploads/{uploadId}/abort", async ([FromRoute] string bucketId, [FromRoute] string objectId, [FromRoute] string uploadId, HttpContext httpContext,
                [FromBody] AbortMultipartUploadContract contract,
                [FromServices] IMediator mediator) => await mediator.Send(AbortMultipartUploadContract.ToCommand(httpContext.GetUserId(), bucketId, objectId, uploadId)))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();

        app.MapPost("buckets/{bucketId}/objects/{objectId}/uploads/{uploadId}/complete", async ([FromRoute] string bucketId, [FromRoute] string objectId, [FromRoute] string uploadId, HttpContext httpContext,
                [FromBody] CompleteMultipartUploadContract contract,
                [FromServices] IMediator mediator) => await mediator.Send(contract.ToCommand(httpContext.GetUserId(), bucketId, objectId, uploadId)))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();

        app.MapGet("buckets/{bucketId}/objects/{objectId}/uploads/{uploadId}/signed-urls", async ([FromRoute] string bucketId, [FromRoute] string objectId, [FromRoute] string uploadId, HttpContext httpContext,
                [FromQuery] long fileSize, [FromServices] IMediator mediator) => await mediator.Send(new GetUploadSignedUrlsQuery(httpContext.GetUserId(), bucketId, objectId, uploadId, fileSize)))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();

        app.MapGet("buckets/{bucketName}/objects/{objectId}", async ([FromRoute] string bucketName, [FromRoute] string objectId, HttpContext httpContext,
                [FromServices] IMediator mediator) => await mediator.Send(new GetObjectQuery(httpContext.GetUserId(), bucketName, objectId)))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();

        app.MapPost("buckets/{bucketId}/objects/filter", async (HttpContext httpContext, [FromRoute] string bucketId, [FromBody] GetObjectsContract contract,
                [FromServices] IMediator mediator) => await mediator.Send(contract.ToQuery(httpContext.GetUserId(), bucketId)))
            .WithGroupName(Source)
            .DisableAntiforgery()
            .RequireAuthorization();
    }
}
