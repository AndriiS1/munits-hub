using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.UseCases.Buckets.Services;
namespace MunitSHub.UseCases.Buckets.Queries.GetBuckets;

public class GetsBucketCommandHandler(IBucketClientManager bucketClientManager) : IRequestHandler<GetBucketsCommand, IResult>
{
    public async Task<IResult> Handle(GetBucketsCommand command, CancellationToken cancellationToken)
    {
        var response = await bucketClientManager.GetClient().GetBucketsAsync(new GetBucketsRequest
        {
            BucketNames = {command.Names},
        }, cancellationToken: cancellationToken);
        
        return Results.Ok(response.Content);
    }
}
