using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Objects.Services;
namespace MunitSHub.UseCases.Objects.Queries.GetObjects;

public class GetObjectsQueryHandler(IObjectClientManager objectClient, IPermissionRepository permissionRepository)
    : IRequestHandler<GetObjectsQuery, IResult>
{
    public async Task<IResult> Handle(GetObjectsQuery query, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.Get(query.UserId, query.BucketId, TargetType.Bucket);

        if (permission == null) return Results.Forbid();

        var response = await objectClient.GetClient().GetObjectsByPrefixAsync(new GetObjectByPrefixRequest
        {
            BucketId = query.BucketId,
            Prefix = query.Prefix,
            PageSize = query.PageSize,
            Cursor = ParseCursor(query.Cursor)

        }, cancellationToken: cancellationToken);

        return Results.Ok(response.Suffixes);
    }

    private static ObjectSuffixesCursor? ParseCursor(GetObjectsCursor? cursor)
    {
        return cursor is null ? null : new ObjectSuffixesCursor
        {
            Suffix = cursor.Suffix,
            Type = cursor.Type
        };
    }
}
