using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.UseCases.Buckets.Services;
namespace MunitSHub.UseCases.Buckets.Commands.Delete;

public class DeleteBucketCommandHandler(IBucketClientManager clientManager) : IRequestHandler<DeleteBucketCommand, IResult>
{
    public async Task<IResult> Handle(DeleteBucketCommand command, CancellationToken cancellationToken)
    {
        await clientManager.GetClient().DeleteBucketAsync(new DeleteBucketRequest
        {
            BucketName = command.Name,
        }, cancellationToken: cancellationToken);
        
        return Results.NoContent();
    }
}
