using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Buckets.Services;
namespace MunitSHub.UseCases.Buckets.Queries.GetUserBuckets;

public class GetUserBucketCommandHandler(IBucketClientManager bucketClientManager, IPermissionRepository permissionRepository)
    : IRequestHandler<GetUserBucketsCommand, IResult>
{
    public async Task<IResult> Handle(GetUserBucketsCommand command, CancellationToken cancellationToken)
    {
        var userBucketPermissions = await permissionRepository.GetAll(command.UserId, command.Page, command.PageSize, TargetType.Bucket);

        var response = await bucketClientManager.GetClient().GetBucketsAsync(new GetBucketsRequest
        {
            Ids =
            {
                userBucketPermissions.Select(p => p.TargetId)
            }
        }, cancellationToken: cancellationToken);

        return Results.Ok(response.Content);
    }
}
