using MongoDB.Driver;
namespace MunitSHub.Domain.Permission;

public interface IPermissionRepository
{
    Task Create(User.User user, IClientSessionHandle session);
}
