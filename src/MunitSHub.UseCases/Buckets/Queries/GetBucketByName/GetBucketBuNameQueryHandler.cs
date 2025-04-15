using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Buckets.Services;
namespace MunitSHub.UseCases.Buckets.Queries.GetBucketByName;

public class GetBucketBuNameQueryHandler(IPermissionRepository permissionRepository,
    IBucketClientManager bucketClientManager) : IRequestHandler<GetBucketByNameQuery, IResult>
{
    public async Task<IResult> Handle(GetBucketByNameQuery query, CancellationToken cancellationToken)
    {
        var userPermission = await permissionRepository.GetByName(query.UserId, query.Name, TargetType.Bucket);

        if (userPermission == null)
        {
            return Results.Forbid();
        }

        var response = await bucketClientManager.GetClient().GetBucketAsync(new GetBucketRequest
        {
            Id = userPermission.TargetId
        }, cancellationToken: cancellationToken);

        return Results.Ok(response);
    }
}
