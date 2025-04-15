using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Objects.Services;
namespace MunitSHub.UseCases.Objects.Queries.GetUploadSignedUrls;

public class GetUploadSignedUrlsQueryHandler(IObjectClientManager objectClient, IPermissionRepository permissionRepository)
    : IRequestHandler<GetUploadSignedUrlsQuery, IResult>
{
    private const int PartSize = 5 * 1024 * 1024;

    public async Task<IResult> Handle(GetUploadSignedUrlsQuery query, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.Get(query.UserId, query.BucketId, TargetType.Bucket);

        if (permission == null) return Results.Forbid();

        var totalParts = (int)Math.Ceiling((double)query.FileSize / PartSize);

        var urlTasks = new List<Task<string>>();

        for (var partNumber = 1; partNumber <= totalParts; partNumber++)
        {
            urlTasks.Add(FetchSignedUrlAsync(query.BucketId, query.ObjectId, query.UploadId, partNumber, cancellationToken));
        }

        var urls = await Task.WhenAll(urlTasks);

        return Results.Ok(new
        {
            Urls = urls
        });
    }

    private async Task<string> FetchSignedUrlAsync(string bucketId, string objectId, string uploadId, int partNumber, CancellationToken cancellationToken)
    {
        var response = await objectClient.GetClient().GetPartUploadUrlAsync(new GetPartUploadUrlRequest
        {
            BucketId = bucketId,
            ObjectId = objectId,
            PartNumber = partNumber,
            UploadId = uploadId
        }, cancellationToken: cancellationToken);

        return response.Url;
    }
}
