using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Objects.Commands.AbortMultipartUpload;

public sealed record AbortMultipartUploadCommand(ObjectId UserId, string BucketId, string UploadId) : IRequest<IResult>;
