using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.UseCases.Buckets.Services;
namespace MunitSHub.UseCases.Buckets.Queries.BucketExists;

public class BucketExistsQueryHandler(IBucketClientManager bucketClientManager) : IRequestHandler<BucketExistsQuery, IResult>
{
    public async Task<IResult> Handle(BucketExistsQuery query, CancellationToken cancellationToken)
    {
        var response = await bucketClientManager.GetClient().BucketExistsCheckAsync(new BucketExistsCheckRequest
        {
            BucketName = query.Name
        }, cancellationToken: cancellationToken);

        return Results.Ok(new
        {
            response.Exists
        });
    }
}
