using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Buckets.Queries.GetBucketByName;

public sealed record GetBucketByNameQuery(ObjectId UserId, string Name) : IRequest<IResult>;
