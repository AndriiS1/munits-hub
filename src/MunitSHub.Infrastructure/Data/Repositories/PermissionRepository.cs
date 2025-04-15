using MongoDB.Bson;
using MongoDB.Driver;
using MunitSHub.Domain.Permission;
namespace MunitSHub.Infrastructure.Data.Repositories;

public class PermissionRepository(IMongoDatabase database) : IPermissionRepository
{
    private readonly IMongoCollection<Permission> _collection = database.GetCollection<Permission>(Collections.Permissions);
    private const string PermissionSearchIndex = "PermissionSearchIndex";
    
    public async Task Create(Permission permission)
    {
        await _collection.InsertOneAsync(permission);
    }

    public async Task<Permission?> Get(ObjectId userId, string targetId, TargetType targetType)
    {
        return await (await _collection
            .FindAsync(x => x.UserId == userId && x.TargetId == targetId && x.TargetType == targetType)).FirstOrDefaultAsync();
    }
    
    public async Task<Permission?> GetByName(ObjectId userId, string targetName, TargetType targetType)
    {
        return await (await _collection
            .FindAsync(x => x.UserId == userId && x.TargetName == targetName && x.TargetType == targetType)).FirstOrDefaultAsync();
    }

    public async Task<List<Permission>> GetAll(ObjectId userId, int page, int pageSize, TargetType targetType)
    {
        var filter = Builders<Permission>.Filter.Where(p => p.UserId == userId && p.TargetType == targetType);

        return await (await _collection.FindAsync(filter, new FindOptions<Permission>
        {
            Skip = (page - 1) * pageSize,
            Limit = pageSize,
            Sort = Builders<Permission>.Sort.Descending(x => x.Created)
        })).ToListAsync();
    }
    
    public async Task<List<Permission>> Search(ObjectId userId, int page, int pageSize, TargetType targetType, string search)
    {
        var filter = Builders<Permission>.Filter.Where(p => p.UserId == userId && p.TargetType == targetType);

        var autocomplete = Builders<Permission>.Search.Compound()
            .Must(Builders<Permission>.Search.Autocomplete(c => c.TargetName, search));
        
        return await _collection.Aggregate()
            .Search(autocomplete, indexName:PermissionSearchIndex)
            .Match(filter).Limit(pageSize)
            .Skip((page - 1) * pageSize)
            .ToListAsync();
    }
}
