using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Objects.Services;
namespace MunitSHub.UseCases.Objects.Commands.Complete;

public class CompleteMultipartUploadCommandHandler(IObjectClientManager objectClient, IPermissionRepository permissionRepository)
    : IRequestHandler<CompleteMultipartUploadCommand, IResult>
{
    public async Task<IResult> Handle(CompleteMultipartUploadCommand command, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.Get(command.UserId, command.BucketId, TargetType.Bucket);

        if (permission == null) return Results.Forbid();

        var request = new CompleteMultipartUploadRequest
        {
            BucketId = command.BucketId,
            ObjectId = command.ObjectId,
            UploadId = command.UploadId
        };
        
        foreach (var entry in command.ETags)
        {
            request.ETags.Add(entry.Key, entry.Value);
        }

        await objectClient.GetClient().CompleteMultipartUploadAsync(request, cancellationToken: cancellationToken);

        return Results.NoContent();
    }
}
