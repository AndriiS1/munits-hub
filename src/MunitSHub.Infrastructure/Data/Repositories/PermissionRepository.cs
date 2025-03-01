using MongoDB.Driver;
using MunitSDomain.Infrastructure.Data;
using MunitSHub.Domain.Permission;
using MunitSHub.Domain.User;
namespace MunitSHub.Infrastructure.Data.Repositories;

public class PermissionRepository(IMongoDatabase database) : IPermissionRepository
{
    private readonly IMongoCollection<User> _collection = database.GetCollection<User>(Collections.Users);
    
    public async Task Create(User user, IClientSessionHandle session)
    {
        await _collection.InsertOneAsync(session, user);
    }
}
