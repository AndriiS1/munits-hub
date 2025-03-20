using MongoDB.Driver;
using MunitSHub.Domain.Permission;
using MunitSHub.Domain.User;
namespace MunitSHub.Infrastructure.Data.Repositories;

public class PermissionRepository(IMongoDatabase database) : IPermissionRepository
{
    private readonly IMongoCollection<Permission> _collection = database.GetCollection<Permission>(Collections.Permissions);
    
    public async Task Create(Permission permission)
    {
        await _collection.InsertOneAsync(permission);
    }
}
