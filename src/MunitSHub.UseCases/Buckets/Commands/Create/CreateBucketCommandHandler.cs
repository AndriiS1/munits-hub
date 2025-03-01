using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.UseCases.Buckets.Services;
namespace MunitSHub.UseCases.Buckets.Commands.Create;

public class CreateBucketCommandHandler(IBucketClientManager clientManager) : IRequestHandler<CreateBucketCommand, IResult>
{
    public async Task<IResult> Handle(CreateBucketCommand command, CancellationToken cancellationToken)
    {
        await clientManager.GetClient().CreateBucketAsync(new CreateBucketRequest
        {
            BucketName = command.Name,
            VersioningEnabled = command.VersioningEnabled,
            VersionsLimit = command.VersionsLimit
        }, cancellationToken: cancellationToken);
        
        return Results.NoContent();
    }
}
