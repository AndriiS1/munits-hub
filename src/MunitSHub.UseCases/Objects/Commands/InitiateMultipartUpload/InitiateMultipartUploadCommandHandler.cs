using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Objects.Services;
namespace MunitSHub.UseCases.Objects.Commands.InitiateMultipartUpload;

public class InitiateMultipartUploadCommandHandler(IObjectClientManager objectClient, IPermissionRepository permissionRepository)
    : IRequestHandler<InitiateMultipartUploadCommand, IResult>
{
    public async Task<IResult> Handle(InitiateMultipartUploadCommand command, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.Get(command.UserId, command.BucketId, TargetType.Bucket);

        if (permission == null) return Results.Forbid();

        var response = await objectClient.GetClient().InitiateMultipartUploadAsync(new InitiateMultipartUploadRequest
        {
            BucketId = command.BucketId,
            FileKey = command.FileKey,
            MimeType = command.ContentType,
            SizeInBytes = command.SizeInBytes
        }, cancellationToken: cancellationToken);

        return Results.Ok(new
        {
            response.ObjectId,
            response.UploadId
        });
    }
}
