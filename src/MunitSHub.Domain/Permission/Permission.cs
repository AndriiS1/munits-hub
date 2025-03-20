using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace MunitSHub.Domain.Permission;

public class Permission
{
    [BsonId]
    public required ObjectId Id { get; set; }
    public required ObjectId UserId { get; set; }
    public required string TargetId { get; set; }
    public required TargetType TargetType { get; set; }
}
