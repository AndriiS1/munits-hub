using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MunitSDomain.Infrastructure.Data.Repositories;
using MunitSDomain.Infrastructure.Options.DataBase;
using MunitSDomain.Infrastructure.Options.Storage;
using MunitSHub.Domain.Permission;
using MunitSHub.Domain.User;
namespace MunitSDomain.Infrastructure;

public static class InfrastructureExtensions
{
    public static void ConfigureInfrastructure(this WebApplicationBuilder builder)
    {
        builder.AddOptions();
    }
    
    private static void AddOptions(this WebApplicationBuilder builder)
    {
        builder.ConfigureStorageOptions();
        builder.ConfigureDatabaseOptions();
        builder.ConfigureRepositories();
    }
    

    private static void ConfigureRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPermissionRepository, PermissionRepository>();
        builder.Services.AddSingleton<IUserRepository, UserRepository>();
    }
}
