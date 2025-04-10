using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Objects.Services;
namespace MunitSHub.UseCases.Objects.Commands.AbortMultipartUpload;

public class AbortMultipartUploadCommandHandler(IObjectClientManager objectClient, IPermissionRepository permissionRepository)
    : IRequestHandler<AbortMultipartUploadCommand, IResult>
{
    public async Task<IResult> Handle(AbortMultipartUploadCommand command, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.Get(command.UserId, command.BucketId, TargetType.Bucket);

        if (permission == null) return Results.Forbid();

        await objectClient.GetClient().AbortMultipartUploadAsync(new AbortMultipartUploadRequest
        {
            BucketId = command.BucketId,
            UploadId = command.UploadId
        }, cancellationToken: cancellationToken);

        return Results.NoContent();
    }
}
