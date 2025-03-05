using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace MunitSHub.Domain.User;

public class User
{
    [BsonId]
    public ObjectId Id { get; set; }
    public required string Email { get; init; }
    public required string PasswordHash { get; init; }
    public required string RefreshToken { get; init; }
    public required DateTime RefreshTokenExpiryTime { get; init; }
}
