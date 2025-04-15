using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Buckets.Services;
namespace MunitSHub.UseCases.Buckets.Queries.GetUserBuckets;

public class GetUserBucketQueryHandler(IBucketClientManager bucketClientManager, IPermissionRepository permissionRepository)
    : IRequestHandler<GetUserBucketsQuery, IResult>
{
    public async Task<IResult> Handle(GetUserBucketsQuery query, CancellationToken cancellationToken)
    {
        var userBucketPermissions = await permissionRepository.GetAll(query.UserId, query.Page, query.PageSize, TargetType.Bucket);

        var response = await bucketClientManager.GetClient().GetBucketsAsync(new GetBucketsRequest
        {
            Ids =
            {
                userBucketPermissions.Select(p => p.TargetId)
            }
        }, cancellationToken: cancellationToken);

        return Results.Ok(response.Buckets);
    }
}
