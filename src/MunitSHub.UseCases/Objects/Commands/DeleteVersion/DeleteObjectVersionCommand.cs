using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Objects.Commands.DeleteVersion;

public sealed record DeleteObjectVersionCommand(ObjectId UserId, string BucketId, string FileKey, string UploadId) : IRequest<IResult>;
