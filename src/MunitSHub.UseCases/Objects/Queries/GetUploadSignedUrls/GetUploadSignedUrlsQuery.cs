using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Objects.Queries.GetUploadSignedUrls;

public sealed record GetUploadSignedUrlsQuery(ObjectId UserId, string BucketId, string UploadId, long FileSize) : IRequest<IResult>;
