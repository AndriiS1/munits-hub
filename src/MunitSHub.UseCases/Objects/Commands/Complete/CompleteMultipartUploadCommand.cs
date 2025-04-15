using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Objects.Commands.Complete;

public sealed record CompleteMultipartUploadCommand(ObjectId UserId, string BucketId, string ObjectId, string UploadId, Dictionary<int, string> ETags) : IRequest<IResult>;
