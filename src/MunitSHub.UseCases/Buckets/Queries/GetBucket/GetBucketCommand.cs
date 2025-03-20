using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Buckets.Queries.GetBucket;

public sealed record GetBucketCommand(ObjectId UserId, string BucketId) : IRequest<IResult>;
