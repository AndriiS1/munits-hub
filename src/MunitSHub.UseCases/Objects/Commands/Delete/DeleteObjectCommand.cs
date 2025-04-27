using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Objects.Commands.Delete;

public sealed record DeleteObjectCommand(ObjectId UserId, string BucketId, string FileKey) : IRequest<IResult>;
