using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Buckets.Services;
namespace MunitSHub.UseCases.Buckets.Queries.GetMetrics;

public class GetMetricsQueryHandler(IPermissionRepository permissionRepository,
    IBucketClientManager bucketClientManager) : IRequestHandler<GetMetricsQuery, IResult>
{
    public async Task<IResult> Handle(GetMetricsQuery query, CancellationToken cancellationToken)
    {
        var userPermission = await permissionRepository.Get(query.UserId, query.BucketId, TargetType.Bucket);

        if (userPermission == null)
        {
            return Results.Forbid();
        }

        var response = await bucketClientManager.GetClient().GetBucketMetricsAsync(new GetMetricsRequest
        {
            BucketId = query.BucketId
        }, cancellationToken: cancellationToken);

        return Results.Ok(response);
    }
}
