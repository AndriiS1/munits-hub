using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.UseCases.Buckets.Services;
namespace MunitSHub.UseCases.Buckets.Queries.GetBucket;

public class GetBucketCommandHandler(IBucketClientManager bucketClientManager) : IRequestHandler<GetBucketCommand, IResult>
{
    public async Task<IResult> Handle(GetBucketCommand command, CancellationToken cancellationToken)
    {
        var response = await bucketClientManager.GetClient().GetBucketAsync(new GetBucketRequest
        {
            BucketName = command.Name,
        }, cancellationToken: cancellationToken);
        
        
        return Results.Ok(response.Content);
    }
}
