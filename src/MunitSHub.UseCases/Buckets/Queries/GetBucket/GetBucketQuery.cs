using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Buckets.Queries.GetBucket;

public sealed record GetBucketQuery(ObjectId UserId, string BucketId) : IRequest<IResult>;
