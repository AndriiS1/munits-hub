using MediatR;
using Microsoft.AspNetCore.Http;
namespace MunitSHub.UseCases.Buckets.Queries.GetBucket;

public sealed record GetBucketCommand(string Name) : IRequest<IResult>;
