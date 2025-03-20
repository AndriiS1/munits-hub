using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Buckets.Services;
namespace MunitSHub.UseCases.Buckets.Queries.GetBucket;

public class GetBucketCommandHandler(IPermissionRepository permissionRepository,
    IBucketClientManager bucketClientManager) : IRequestHandler<GetBucketCommand, IResult>
{
    public async Task<IResult> Handle(GetBucketCommand command, CancellationToken cancellationToken)
    {
        var userPermission = await permissionRepository.Get(command.UserId, command.BucketId, TargetType.Bucket);

        if (userPermission == null)
        {
            return Results.Forbid();
        }

        var response = await bucketClientManager.GetClient().GetBucketAsync(new GetBucketRequest
        {
            Id = command.BucketId
        }, cancellationToken: cancellationToken);

        return Results.Ok(response.Content);
    }
}
