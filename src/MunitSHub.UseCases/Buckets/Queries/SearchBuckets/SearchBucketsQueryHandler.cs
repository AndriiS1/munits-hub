using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Buckets.Services;
namespace MunitSHub.UseCases.Buckets.Queries.SearchBuckets;

public class SearchBucketsQueryHandler(IBucketClientManager bucketClientManager, IPermissionRepository permissionRepository)
    : IRequestHandler<SearchBucketsQuery, IResult>
{
    public async Task<IResult> Handle(SearchBucketsQuery query, CancellationToken cancellationToken)
    {
        var userBucketPermissions = await permissionRepository.Search(query.UserId, query.Page, query.PageSize, TargetType.Bucket, query.Search);

        var response = await bucketClientManager.GetClient().GetBucketsAsync(new GetBucketsRequest
        {
            Ids =
            {
                userBucketPermissions.Select(p => p.TargetId)
            }
        }, cancellationToken: cancellationToken);

        return Results.Ok(response.Content);
    }
}
