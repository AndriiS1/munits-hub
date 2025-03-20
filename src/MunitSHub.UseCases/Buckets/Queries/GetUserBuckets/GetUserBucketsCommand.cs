using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Buckets.Queries.GetUserBuckets;

public sealed record GetUserBucketsCommand(ObjectId UserId, int PageSize, int Page) : IRequest<IResult>;
