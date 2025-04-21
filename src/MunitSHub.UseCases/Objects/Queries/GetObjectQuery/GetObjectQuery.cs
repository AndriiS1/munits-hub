using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Objects.Queries.GetObjectQuery;

public sealed record GetObjectQuery(ObjectId UserId, string BucketName, string ObjectId) : IRequest<IResult>;
