using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Buckets.Commands.Create;

public sealed record CreateBucketCommand(ObjectId UserId, string Name, bool VersioningEnabled, int VersionsLimit) : IRequest<IResult>;
