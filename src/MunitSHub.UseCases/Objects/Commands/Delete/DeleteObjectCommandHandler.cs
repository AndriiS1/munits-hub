using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Objects.Services;
namespace MunitSHub.UseCases.Objects.Commands.Delete;

public class DeleteObjectCommandHandler(IObjectClientManager clientManager, IPermissionRepository permissionRepository)
    : IRequestHandler<DeleteObjectCommand, IResult>
{
    public async Task<IResult> Handle(DeleteObjectCommand command, CancellationToken cancellationToken)
    {
        var userPermission = await permissionRepository.Get(command.UserId, command.BucketId, TargetType.Bucket);

        if (userPermission == null)
        {
            return Results.Forbid();
        }

        await clientManager.GetClient().DeleteObjectAsync(new DeleteObjectRequest
        {
            BucketId = command.BucketId,
            FileKey = command.FileKey
        }, cancellationToken: cancellationToken);

        return Results.NoContent();
    }
}
