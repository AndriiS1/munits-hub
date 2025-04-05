using MongoDB.Bson;
using MunitSHub.UseCases.Objects.Queries.GetObjects;
namespace MunitSHub.Apis.Objects.Contracts;

public sealed record GetObjectContract
{
    public required string BucketId { get; init; }
    public required string Prefix { get; init; }

    public GetObjectsQuery ToQuery(ObjectId userId)
    {
        return new GetObjectsQuery(userId, BucketId, Prefix);
    }
}
