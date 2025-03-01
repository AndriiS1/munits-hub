using System.Security.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
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
        builder.ConfigureDatabase();
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

    private static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        
        var optionsSection = configuration.GetSection(DataBaseOptions.Section);
        var options = optionsSection.Get<DataBaseOptions>();
        
        var settings = MongoClientSettings.FromUrl(new MongoUrl(options!.ConnectionString));
        settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
        builder.Services.AddSingleton<IMongoClient>(new MongoClient(settings));
    }
}
