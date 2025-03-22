using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace MunitSHub.Domain.Entity;

public abstract class Entity
{
    [BsonId]
    public required ObjectId Id { get; init; }
    public DateTime Created { get; private set; } = DateTime.UtcNow;
}
