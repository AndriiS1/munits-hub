using MongoDB.Driver;
using MunitSHub.Domain.Permission;
using MunitSHub.Domain.User;
namespace MunitSDomain.Infrastructure.Data.Repositories;

public class UserRepository(IMongoDatabase database) : IUserRepository
{
    private readonly IMongoCollection<Permission> _collection = database.GetCollection<Permission>(Collections.Permissions);
    
    public async Task Create(Permission permission, IClientSessionHandle session)
    {
        await _collection.InsertOneAsync(session, permission);
    }   
}
