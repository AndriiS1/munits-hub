using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Buckets.Commands.Delete;

public sealed record DeleteBucketCommand(ObjectId UserId, string BucketId) : IRequest<IResult>;
