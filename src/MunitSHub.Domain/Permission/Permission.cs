using MongoDB.Bson;
namespace MunitSHub.Domain.Permission;

public class Permission : Entity.Entity
{
    public required ObjectId UserId { get; init; }
    public required string TargetId { get; init; }
    public required string TargetName { get; init; }
    public required TargetType TargetType { get; init; }
}
