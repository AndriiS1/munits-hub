using MongoDB.Bson;
namespace MunitSHub.Domain.User;

public interface IUserRepository
{
    Task Create(User user);
    Task<User?> Get(string email, string passwordHash);
    Task<User?> Get(ObjectId userId);
    Task<User?> Get(string email);
    Task UpdateUserRefreshTokenData(ObjectId userId, string refreshToken, DateTime refreshTokenExpiryDate);
}
