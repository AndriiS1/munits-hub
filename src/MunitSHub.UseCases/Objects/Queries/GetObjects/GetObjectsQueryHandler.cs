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

        var response = await objectClient.GetClient().GetObjectByPrefixAsync(new GetObjectByPrefixRequest
        {
            BucketId = query.BucketId,
            Prefix = query.Prefix
        }, cancellationToken: cancellationToken);

        return Results.Ok(response.Content);
    }
}
