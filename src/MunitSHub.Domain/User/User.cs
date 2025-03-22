namespace MunitSHub.Domain.User;

public class User : Entity.Entity
{
    public required string Email { get; init; }
    public required string PasswordHash { get; init; }
    public required string RefreshToken { get; init; }
    public required DateTime RefreshTokenExpiryTime { get; init; }
}
