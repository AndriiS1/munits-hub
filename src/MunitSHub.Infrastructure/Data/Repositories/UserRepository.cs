using MongoDB.Bson;
using MongoDB.Driver;
using MunitSHub.Domain.User;
namespace MunitSHub.Infrastructure.Data.Repositories;

public class UserRepository(IMongoDatabase database) : IUserRepository
{
    private readonly IMongoCollection<User> _collection = database.GetCollection<User>(Collections.Users);
    
    public async Task Create(User user)
    {
        await _collection.InsertOneAsync(user);
    }
    
    public async Task<User?> Get(string email, string passwordHash)
    {
        var filter = new FilterDefinitionBuilder<User>().Where(u => passwordHash == u.PasswordHash && u.Email == email);
        
        return await (await _collection.FindAsync(filter)).FirstOrDefaultAsync();
    }
    
    public async Task<User?> Get(ObjectId userId)
    {
        var filter = new FilterDefinitionBuilder<User>().Where(u => u.Id == userId);
        
        return await (await _collection.FindAsync(filter)).FirstOrDefaultAsync();
    }
    
    public async Task<User?> Get(string email)
    {
        var filter = new FilterDefinitionBuilder<User>().Where(u => u.Email == email);
        
        return await (await _collection.FindAsync(filter)).FirstOrDefaultAsync();
    }

    public async Task UpdateUserRefreshTokenData(ObjectId userId, string refreshToken, DateTime refreshTokenExpiryDate)
    {
        var filter = new FilterDefinitionBuilder<User>().Where(u => u.Id == userId);
        
        var update = new UpdateDefinitionBuilder<User>()
            .Set(r => r.RefreshToken, refreshToken)
            .Set(r => r.RefreshTokenExpiryTime, refreshTokenExpiryDate);
        
        await _collection.UpdateOneAsync(filter, update);
    }
}
