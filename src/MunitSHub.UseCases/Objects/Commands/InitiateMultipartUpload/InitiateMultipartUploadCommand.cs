using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Objects.Commands.InitiateMultipartUpload;

public sealed record InitiateMultipartUploadCommand(ObjectId UserId, string BucketId, string FileKey, long SizeInBytes, string ContentType) : IRequest<IResult>;
