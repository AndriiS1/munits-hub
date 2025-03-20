using MongoDB.Bson;
namespace MunitSHub.Domain.Permission;

public interface IPermissionRepository
{
    Task Create(Permission permission);
    Task<Permission?> Get(ObjectId userId, string targetId, TargetType targetType);
    Task<List<Permission>> GetAll(ObjectId userId, int page, int pageSize, TargetType targetType);
}
