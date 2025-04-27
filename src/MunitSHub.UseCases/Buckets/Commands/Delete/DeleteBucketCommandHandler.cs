using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Buckets.Services;
namespace MunitSHub.UseCases.Buckets.Commands.Delete;

public class DeleteBucketCommandHandler(IBucketClientManager clientManager, IPermissionRepository permissionRepository)
    : IRequestHandler<DeleteBucketCommand, IResult>
{
    public async Task<IResult> Handle(DeleteBucketCommand command, CancellationToken cancellationToken)
    {
        var userPermission = await permissionRepository.Get(command.UserId, command.BucketId, TargetType.Bucket);

        if (userPermission == null)
        {
            return Results.Forbid();
        }

        await permissionRepository.Delete(userPermission.Id);
        await clientManager.GetClient().DeleteBucketAsync(new DeleteBucketRequest
        {
            Id = command.BucketId,
        }, cancellationToken: cancellationToken);
        
        return Results.NoContent();
    }
}
