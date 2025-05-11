using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Buckets.Queries.GetMetrics;

public sealed record GetMetricsQuery(ObjectId UserId, string BucketId) : IRequest<IResult>;
