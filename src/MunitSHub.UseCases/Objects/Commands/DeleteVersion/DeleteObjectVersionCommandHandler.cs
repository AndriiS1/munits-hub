using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Objects.Services;
namespace MunitSHub.UseCases.Objects.Commands.DeleteVersion;

public class DeleteObjectVersionCommandHandler(IObjectClientManager clientManager, IPermissionRepository permissionRepository)
    : IRequestHandler<DeleteObjectVersionCommand, IResult>
{
    public async Task<IResult> Handle(DeleteObjectVersionCommand command, CancellationToken cancellationToken)
    {
        var userPermission = await permissionRepository.Get(command.UserId, command.BucketId, TargetType.Bucket);

        if (userPermission == null)
        {
            return Results.Forbid();
        }

        await clientManager.GetClient().DeleteObjectVersionAsync(new DeleteObjectVersionRequest
        {
            BucketId = command.BucketId,
            FileKey = command.FileKey,
            UploadId = command.UploadId
        }, cancellationToken: cancellationToken);

        return Results.NoContent();
    }
}
