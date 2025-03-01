using MediatR;
using Microsoft.AspNetCore.Http;
namespace MunitSHub.UseCases.Buckets.Queries.GetBuckets;

public sealed record GetBucketsCommand(string[] Names) : IRequest<IResult>;
