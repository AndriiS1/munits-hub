using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Buckets.Queries.SearchBuckets;

public sealed record SearchBucketsQuery(ObjectId UserId, int PageSize, int Page, string Search) : IRequest<IResult>;
