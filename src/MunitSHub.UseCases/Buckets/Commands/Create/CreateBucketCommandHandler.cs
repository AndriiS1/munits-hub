using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Buckets.Services;
namespace MunitSHub.UseCases.Buckets.Commands.Create;

public class CreateBucketCommandHandler(IBucketClientManager clientManager, IPermissionRepository permissionRepository) : IRequestHandler<CreateBucketCommand, IResult>
{
    public async Task<IResult> Handle(CreateBucketCommand command, CancellationToken cancellationToken)
    {
        var createBucketResponse = await clientManager.GetClient().CreateBucketAsync(new CreateBucketRequest
        {
            BucketName = command.Name,
            VersioningEnabled = command.VersioningEnabled,
            VersionsLimit = command.VersionsLimit
        }, cancellationToken: cancellationToken);


        await permissionRepository.Create(new Permission
        {
            Id = new ObjectId(),
            TargetId = createBucketResponse.BucketId,
            TargetType = TargetType.Bucket,
            UserId = command.UserId,
            TargetName = command.Name,
        });
        
        return Results.NoContent();
    }
}
