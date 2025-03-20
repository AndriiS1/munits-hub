using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Buckets.Services;
namespace MunitSHub.UseCases.Buckets.Queries.GetBucket;

public class GetBucketQueryHandler(IPermissionRepository permissionRepository,
    IBucketClientManager bucketClientManager) : IRequestHandler<GetBucketQuery, IResult>
{
    public async Task<IResult> Handle(GetBucketQuery query, CancellationToken cancellationToken)
    {
        var userPermission = await permissionRepository.Get(query.UserId, query.BucketId, TargetType.Bucket);

        if (userPermission == null)
        {
            return Results.Forbid();
        }

        var response = await bucketClientManager.GetClient().GetBucketAsync(new GetBucketRequest
        {
            Id = query.BucketId
        }, cancellationToken: cancellationToken);

        return Results.Ok(response.Content);
    }
}
