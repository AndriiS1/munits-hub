using MongoDB.Driver;
namespace MunitSHub.Domain.User;

public interface IUserRepository
{
    Task Create(Permission.Permission permission, IClientSessionHandle session);
}
