using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Objects.Queries.GetObjects;

public sealed record GetObjectsQuery(ObjectId UserId, string BucketId, string Prefix) : IRequest<IResult>;
