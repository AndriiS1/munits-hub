using System.Text.Json.Serialization;
using MongoDB.Bson;
using MunitSHub.UseCases.Objects.Queries.GetObjects;
namespace MunitSHub.Apis.Objects.Contracts;

public sealed record GetObjectsContract
{
    public required string Prefix { get; init; }

    [JsonInclude]
    public int PageSize { get; init; } = 20;
    public GetObjectsCursor? Cursor { get; init; }

    public GetObjectsQuery ToQuery(ObjectId userId, string bucketId)
    {
        return new GetObjectsQuery(userId, bucketId, Prefix, PageSize, Cursor);
    }
}
