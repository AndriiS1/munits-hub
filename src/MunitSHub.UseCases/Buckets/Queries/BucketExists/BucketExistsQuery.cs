using MediatR;
using Microsoft.AspNetCore.Http;
namespace MunitSHub.UseCases.Buckets.Queries.BucketExists;

public sealed record BucketExistsQuery(string Name) : IRequest<IResult>;
